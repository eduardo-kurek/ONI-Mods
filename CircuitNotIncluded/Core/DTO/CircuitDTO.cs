using CircuitNotIncluded.Core.Interfaces;
using CircuitNotIncluded.Interfaces;
using KSerialization;
using Newtonsoft.Json.Linq;

namespace CircuitNotIncluded.Core.DTO;

[SerializationConfig(MemberSerialization.OptIn)]
public record CircuitDTO (
	[property: Serialize] string Name,
	[property: Serialize] InputPortDTO[] InputPorts,
	[property: Serialize] OutputPortDTO[] OutputPorts
) : ICircuit, IBlueprintSerializable {

	IInputPort[] ICircuit.InputPorts => InputPorts;
	IOutputPort[] ICircuit.OutputPorts => OutputPorts;
	
	public virtual bool Equals(CircuitDTO? other) {
		if(other is null) return false;
		if(ReferenceEquals(this, other)) return true;

		if(Name != other.Name) return false;
		if(InputPorts.Length != other.InputPorts.Length) return false;
		if(OutputPorts.Length != other.OutputPorts.Length) return false;

		var orderedInputPorts = InputPorts.OrderBy(p => p.Offset.x).ThenBy(p => p.Offset.y);
		var otherOrderedInputPorts = other.InputPorts.OrderBy(p => p.Offset.x).ThenBy(p => p.Offset.y);
		if(!orderedInputPorts.SequenceEqual(otherOrderedInputPorts)) return false;
		
		var orderedOutputPorts = OutputPorts.OrderBy(p => p.Offset.x).ThenBy(p => p.Offset.y);
		var otherOrderedOutputPorts = other.OutputPorts.OrderBy(p => p.Offset.x).ThenBy(p => p.Offset.y);
		if(!orderedOutputPorts.SequenceEqual(otherOrderedOutputPorts)) return false;

		return true;
	}

	public override int GetHashCode(){
		unchecked {
			return (Name?.GetHashCode() ?? 0) ^ InputPorts.Length ^ OutputPorts.Length;
		}
	}

	public JObject ToJson(){
		var inputsArray = new JArray();
		foreach (var port in InputPorts) inputsArray.Add(port.ToJson());

		var outputsArray = new JArray();
		foreach (var port in OutputPorts) outputsArray.Add(port.ToJson());

		return new JObject {
			{ "InputPorts", inputsArray },
			{ "OutputPorts", outputsArray }
		};
	}
	
	public static CircuitDTO FromJson(JObject json){
		string name = json["Name"]?.Value<string>() ?? "Circuit Name";
		var inputPorts = new List<InputPortDTO>();
		var outputPorts = new List<OutputPortDTO>();

		if (json.TryGetValue("InputPorts", out JToken i) && i is JArray inputsArray) {
			foreach (var item in inputsArray)
				if (item is JObject portObj)
					inputPorts.Add(InputPortDTO.FromJson(portObj));
		}

		if (json.TryGetValue("OutputPorts", out JToken o) && o is JArray outputsArray) {
			foreach (var item in outputsArray)
				if (item is JObject portObj)
					outputPorts.Add(OutputPortDTO.FromJson(portObj));
		}

		return new CircuitDTO(name, inputPorts.ToArray(), outputPorts.ToArray());
	}

	public static CircuitDTO Empty() => new("Circuit Name", [], []);
}
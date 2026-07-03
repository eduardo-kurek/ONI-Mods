using KSerialization;
using Newtonsoft.Json.Linq;

namespace CircuitNotIncluded.Core.DTO;

[SerializationConfig(MemberSerialization.OptIn)]
public record CircuitDTO(
	[property: Serialize] string Name,
	[property: Serialize] InputPortDTO[] InputPorts,
	[property: Serialize] RibbonInputDTO[] RibbonInputs,
	[property: Serialize] OutputPortDTO[] OutputPorts,
	[property: Serialize] RibbonOutputDTO[] RibbonOutputs
) {

	public PortDTO[] Ports => InputPorts
		.Concat(RibbonInputs.Cast<PortDTO>()).ToArray()
		.Concat(OutputPorts.Cast<PortDTO>()).ToArray()
		.Concat(RibbonOutputs.Cast<PortDTO>()).ToArray();

	public virtual bool Equals(CircuitDTO? other) {
		if(other is null) return false;
		if(ReferenceEquals(this, other)) return true;

		if(Name != other.Name) return false;
		if(Ports.Length != other.Ports.Length) return false;

		var orderedPorts = Ports.OrderBy(p => p.Offset.x).ThenBy(p => p.Offset.y);
		var otherOrderedPorts = other.Ports.OrderBy(p => p.Offset.x).ThenBy(p => p.Offset.y);
		return orderedPorts.SequenceEqual(otherOrderedPorts);
	}

	public override int GetHashCode(){
		unchecked {
			return (Name?.GetHashCode() ?? 0) ^ Ports.Length;
		}
	}

	public JObject ToJson(){
		var inputPorts = new JArray();
		foreach (var i in InputPorts) inputPorts.Add(i.ToJson());
		
		var ribbonInputs = new JArray();
		foreach (var ri in RibbonInputs) ribbonInputs.Add(ri.ToJson());	
		
		var outputPorts = new JArray();
		foreach (var o in OutputPorts) outputPorts.Add(o.ToJson());
		
		var ribbonOutputs = new JArray();
		foreach (var ro in RibbonOutputs) ribbonOutputs.Add(ro.ToJson());	

		return new JObject {
			{ "Name", Name },
			{ "InputPorts", inputPorts },
			{ "RibbonInputs", ribbonInputs },
			{ "OutputPorts", outputPorts },
			{ "RibbonOutputs", ribbonOutputs }
		};
	}
	
	public static CircuitDTO FromJson(JObject json){
		string name = json["Name"]?.Value<string>() ?? "Circuit Name";
		
		var inputPorts = new List<InputPortDTO>();
		var ribbonInputs = new List<RibbonInputDTO>();
		var outputPorts = new List<OutputPortDTO>();
		var ribbonOutputs = new List<RibbonOutputDTO>();

		if (json.TryGetValue("InputPorts", out JToken i) && i is JArray inputsArray) {
			foreach (var item in inputsArray)
				if (item is JObject portObj)
					inputPorts.Add(InputPortDTO.FromJson(portObj));
		}
		
		if (json.TryGetValue("RibbonInputs", out JToken ri) && ri is JArray ribbonInputsArray) {
			foreach (var item in ribbonInputsArray)
				if (item is JObject portObj)
					ribbonInputs.Add(RibbonInputDTO.FromJson(portObj));
		}
		
		if (json.TryGetValue("OutputPorts", out JToken o) && o is JArray outputsArray) {
			foreach (var item in outputsArray)
				if (item is JObject portObj)
					outputPorts.Add(OutputPortDTO.FromJson(portObj));
		}

		if (json.TryGetValue("RibbonOutputs", out JToken ro) && ro is JArray ribbonOutputsArray) {
			foreach (var item in ribbonOutputsArray)
				if (item is JObject portObj)
					ribbonOutputs.Add(RibbonOutputDTO.FromJson(portObj));
		}

		return new CircuitDTO(
			name, 
			inputPorts.ToArray(), 
			ribbonInputs.ToArray(), 
			outputPorts.ToArray(),
			ribbonOutputs.ToArray()
		);
	}

	public static CircuitDTO Empty() => new("Circuit Name", [], [], [], []);
}
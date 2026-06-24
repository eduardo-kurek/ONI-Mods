using CircuitNotIncluded.Interfaces;
using KSerialization;
using Newtonsoft.Json.Linq;

namespace CircuitNotIncluded.Core.Structs;

[SerializationConfig(MemberSerialization.OptIn)]
public record CircuitDTO (
	[property: Serialize] string Name,
	[property: Serialize] InputPort[] InputPorts,
	[property: Serialize] OutputPort[] OutputPorts
) : IBlueprintSerializable {

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
		var inputPorts = new List<InputPort>();
		var outputPorts = new List<OutputPort>();

		if (json.TryGetValue("InputPorts", out JToken i) && i is JArray inputsArray) {
			foreach (var item in inputsArray)
				if (item is JObject portObj)
					inputPorts.Add(InputPort.FromJson(portObj));
		}

		if (json.TryGetValue("OutputPorts", out JToken o) && o is JArray outputsArray) {
			foreach (var item in outputsArray)
				if (item is JObject portObj)
					outputPorts.Add(OutputPort.FromJson(portObj));
		}

		return new CircuitDTO(name, inputPorts.ToArray(), outputPorts.ToArray());
	}

	public static CircuitDTO Empty() => new("Circuit Name", [], []);
}
using CircuitNotIncluded.Interfaces;
using KSerialization;
using Newtonsoft.Json.Linq;

namespace CircuitNotIncluded.Structs.Ports;


[SerializationConfig(MemberSerialization.OptIn)]
public record CircuitDTO (
	[property: Serialize] string Name,
	[property: Serialize] InputPortDef[] InputPorts,
	[property: Serialize] OutputPortDef[] OutputPorts
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
		var inputPorts = new List<InputPortDef>();
		var outputPorts = new List<OutputPortDef>();

		if (json.TryGetValue("InputPorts", out JToken i) && i is JArray inputsArray) {
			foreach (var item in inputsArray)
				if (item is JObject portObj)
					inputPorts.Add(InputPortDef.FromJson(portObj));
		}

		if (json.TryGetValue("OutputPorts", out JToken o) && o is JArray outputsArray) {
			foreach (var item in outputsArray)
				if (item is JObject portObj)
					outputPorts.Add(OutputPortDef.FromJson(portObj));
		}

		return new CircuitDTO(name, inputPorts.ToArray(), outputPorts.ToArray());
	}

	public static CircuitDTO Empty() => new("Circuit Name", [], []);
}
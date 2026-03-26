using Newtonsoft.Json.Linq;
using UnityEngine;

namespace CircuitNotIncluded.Structs;

public sealed partial class Circuit {
	public static JObject Blueprints_GetData(GameObject source){
		if(!source.TryGetComponent<Circuit>(out var circuit)) return null!;
		var data = new JObject();
		
		var inputs = new JArray();
		foreach (var circuitInput in circuit.Inputs){ 
			var input = circuitInput.ToJson(); 
			inputs.Add(input);
		}

		var outputs = new JArray();
		foreach(var circuitOutput in circuit.Outputs){
			var output = circuitOutput.ToJson();
			outputs.Add(output);
		}

		data["CNIName"] = circuit.CNIName;
		data["Inputs"] = inputs;
		data["Outputs"] = outputs;
		return data;
	}
    
	public static void Blueprints_SetData(GameObject source, JObject data){
		if(!source.TryGetComponent<Circuit>(out var circuit)) return;

		var cniName = data["CNIName"]?.Value<string>() ?? "";
		List<InputPort> inputs = [];
		List<OutputPort> outputs = [];
		
		if (data.TryGetValue("Inputs", out JToken i) && i is JArray inputsArray) {
			foreach (var item in inputsArray) { 
				if (item is JObject inputJson){
					inputs.Add(InputPort.CreateFromJson(inputJson));
				}
			}
		}
		
		if (data.TryGetValue("Outputs", out JToken o) && o is JArray outputsArray) {
			foreach (var item in outputsArray) { 
				if (item is JObject inputJson){
					outputs.Add(OutputPort.CreateFromJson(inputJson));
				}
			}
		}
		
		circuit.SetData(cniName, inputs, outputs);
	}

	public static string Blueprints_ID() => "CircuitNotIncluded_CircuitData";
}
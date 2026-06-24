using CircuitNotIncluded.Core.Structs;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace CircuitNotIncluded.Core;

public sealed partial class Circuit {
	public static JObject Blueprints_GetData(GameObject source){
		return !source.TryGetComponent<Circuit>(out var circuit) ? null! : circuit.dto.ToJson();
	}
    
	public static void Blueprints_SetData(GameObject source, JObject data){
		if(!source.TryGetComponent<Circuit>(out var circuit)) return;
		var circuitDto = CircuitDTO.FromJson(data);
		circuit.SetData(circuitDto);
	}

	public static string Blueprints_ID() => "CircuitNotIncluded_CircuitData";
}
using KSerialization;
using Newtonsoft.Json.Linq;

namespace CircuitNotIncluded.Core.DTO;

[SerializationConfig(MemberSerialization.OptIn)]
public record CircuitDTO (
	[property: Serialize] string Name,
	[property: Serialize] InputPortDTO[] InputPorts,
	[property: Serialize] OutputPortDTO[] OutputPorts
) {

	public PortDTO[] Ports => InputPorts.Cast<PortDTO>().Concat(OutputPorts).ToArray();

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
		var portsArray = new JArray();
		foreach (var port in Ports) portsArray.Add(port.ToJson());

		return new JObject {
			{ "Name", Name },
			{ "Ports", portsArray }
		};
	}
	
	public static CircuitDTO FromJson(JObject json){
		string name = json["Name"]?.Value<string>() ?? "Circuit Name";

		// TODO READ ports with type field discriminator
		// if (json.TryGetValue("InputPorts", out JToken i) && i is JArray inputsArray) {
		// 	foreach (var item in inputsArray)
		// 		if (item is JObject portObj)
		// 			inputPorts.Add(InputPortDTO.FromJson(portObj));
		// }
		//
		// if (json.TryGetValue("OutputPorts", out JToken o) && o is JArray outputsArray) {
		// 	foreach (var item in outputsArray)
		// 		if (item is JObject portObj)
		// 			outputPorts.Add(OutputPortDTO.FromJson(portObj));
		// }

		return new CircuitDTO(name, [], []);
	}

	public static CircuitDTO Empty() => new("Circuit Name", [], []);
}
using CircuitNotIncluded.Core.Interfaces;
using KSerialization;
using Newtonsoft.Json.Linq;

namespace CircuitNotIncluded.Core.DTO;

public record PortDTO(
	[property: Serialize] CellOffset Offset
) : IPort {
	
	protected JObject PortToJson() => new() {
		{ "Offset", JObject.FromObject(Offset) }
	};

	protected static PortDTO PortFromJson(JObject json){
		var offset = json["Offset"]?.ToObject<CellOffset>() ?? default; 
		return new PortDTO(offset);
	}
	
}
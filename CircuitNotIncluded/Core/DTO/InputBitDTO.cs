using CircuitNotIncluded.Core.Interfaces;
using CircuitNotIncluded.Interfaces;
using KSerialization;
using Newtonsoft.Json.Linq;

namespace CircuitNotIncluded.Core.DTO;

[SerializationConfig(MemberSerialization.OptIn)]
public record InputBitDTO (
	[property: Serialize] string Id,
	[property: Serialize] string Description
) : IInputBit, IBlueprintSerializable {
	
	public JObject ToJson() {
		return new JObject {
			{ "Id", Id },
			{ "Description", Description }
		};
	}

	public static InputBitDTO FromJson(JObject json) {
		string id = json["Id"]?.Value<string>() ?? string.Empty;
		string description = json["Description"]?.Value<string>() ?? string.Empty;
		return new InputBitDTO(id, description);
	}
}
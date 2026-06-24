using CircuitNotIncluded.Interfaces;
using KSerialization;
using Newtonsoft.Json.Linq;

namespace CircuitNotIncluded.Core.Structs;

[SerializationConfig(MemberSerialization.OptIn)]
public record InputBit (
	[property: Serialize] string Id,
	[property: Serialize] string Description
) : IBlueprintSerializable {
	public JObject ToJson() {
		return new JObject {
			{ "Id", Id },
			{ "Description", Description }
		};
	}

	public static InputBit FromJson(JObject json) {
		string id = json["Id"]?.Value<string>() ?? string.Empty;
		string description = json["Description"]?.Value<string>() ?? string.Empty;
		return new InputBit(id, description);
	}
}
using CircuitNotIncluded.Interfaces;
using KSerialization;
using Newtonsoft.Json.Linq;

namespace CircuitNotIncluded.Core.Structs;

[SerializationConfig(MemberSerialization.OptIn)]
public record OutputBit (
	[property: Serialize] string Label,
	[property: Serialize] string Description,
	[property: Serialize] string Expression
) : IBlueprintSerializable {
	public JObject ToJson() {
		return new JObject {
			{ "Label", Label },
			{ "Description", Description },
			{ "Expression", Expression }
		};
	}

	public static OutputBit FromJson(JObject json) {
		string label = json["Label"]?.Value<string>() ?? string.Empty;
		string description = json["Description"]?.Value<string>() ?? string.Empty;
		string expression = json["Expression"]?.Value<string>() ?? string.Empty;

		return new OutputBit(label, description, expression);
	}
}
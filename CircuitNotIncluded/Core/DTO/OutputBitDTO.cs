using CircuitNotIncluded.Core.Interfaces;
using CircuitNotIncluded.Interfaces;
using KSerialization;
using Newtonsoft.Json.Linq;

namespace CircuitNotIncluded.Core.DTO;

[SerializationConfig(MemberSerialization.OptIn)]
public record OutputBitDTO (
	[property: Serialize] string Label,
	[property: Serialize] string Description,
	[property: Serialize] string Expression
) : IOutputBit, IBlueprintSerializable {
	
	public JObject ToJson() {
		return new JObject {
			{ "Label", Label },
			{ "Description", Description },
			{ "Expression", Expression }
		};
	}

	public static OutputBitDTO FromJson(JObject json) {
		string label = json["Label"]?.Value<string>() ?? string.Empty;
		string description = json["Description"]?.Value<string>() ?? string.Empty;
		string expression = json["Expression"]?.Value<string>() ?? string.Empty;

		return new OutputBitDTO(label, description, expression);
	}
}
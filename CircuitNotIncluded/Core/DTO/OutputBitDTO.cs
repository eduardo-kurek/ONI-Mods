using System.Text;
using KSerialization;
using Newtonsoft.Json.Linq;

namespace CircuitNotIncluded.Core.DTO;

[SerializationConfig(MemberSerialization.OptIn)]
public record OutputBitDTO (
	[property: Serialize] string Label,
	[property: Serialize] string Description,
	[property: Serialize] string Expression
) {
	
	public string GetDisplayText() {
		StringBuilder sb = new();
		sb.AppendLine($"{Label} = {Utils.UI.ColorizeExpression(Expression)}");
		if(Description.Length > 0)
			sb.AppendLine($"• {Description}");
		return sb.ToString();
	}
	
	public void OnHover(HoverTextDrawer drawer, SelectToolHoverTextCard cfg) {
		drawer.DrawText($"{Label} = {Utils.UI.ColorizeExpression(Expression)}", cfg.Styles_LogicActive.Standard);
		if (Description.IsNullOrWhiteSpace()) return;
		drawer.NewLine();
		drawer.DrawIcon(cfg.iconDash);
		drawer.DrawText(Description, cfg.Styles_BodyText.Standard);
	}
	
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
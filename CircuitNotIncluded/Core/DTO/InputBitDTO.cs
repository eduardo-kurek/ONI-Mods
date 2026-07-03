using System.Text;
using KSerialization;
using Newtonsoft.Json.Linq;

namespace CircuitNotIncluded.Core.DTO;

[SerializationConfig(MemberSerialization.OptIn)]
public record InputBitDTO (
	[property: Serialize] string Id,
	[property: Serialize] string Description
) {
	
	public string GetDisplayText() {
		StringBuilder sb = new();
		sb.AppendLine($"Id: {Id}");
		if(Description.Length > 0)
			sb.AppendLine($"• {Description}");
		return sb.ToString();
	}
	
	public void OnHover(HoverTextDrawer drawer, SelectToolHoverTextCard cfg) {
		drawer.DrawText($"Id: {Id}", cfg.Styles_BodyText.Standard);
		if(Description.Length <= 0) return;
		drawer.NewLine();
		drawer.DrawIcon(cfg.iconDash);
		drawer.DrawText(Description, cfg.Styles_BodyText.Standard);
	}
	
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
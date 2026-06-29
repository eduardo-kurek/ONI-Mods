using KSerialization;
using Newtonsoft.Json.Linq;

namespace CircuitNotIncluded.Core.DTO;

[SerializationConfig(MemberSerialization.OptIn)]
public record InputPortDTO (
	CellOffset Offset,
	[property: Serialize] InputBitDTO Bit1
) : PortDTO(Offset) {
	
	public override void OnHover(string circuitName, HoverTextDrawer drawer, SelectToolHoverTextCard cfg) {
		drawer.DrawText($"INPUT  {Bit1.Id}    <style=\"hovercard_element\">({circuitName.ToUpper()})</style>", cfg.Styles_Title.Standard);
		if(Bit1.Description.IsNullOrWhiteSpace()) return;
		drawer.NewLine();
		drawer.DrawIcon(cfg.iconDash);
		drawer.DrawText($"{Bit1.Description}", cfg.Styles_BodyText.Standard);
	}
   
	public override JObject ToJson() {
		var inputJson = new JObject {
			{ "Bit1", Bit1.ToJson() }
		};
		
		var portJson = base.ToJson();
		portJson.Merge(inputJson);
		return portJson;
	}

	public static InputPortDTO FromJson(JObject json) {
		var bit1 = json.TryGetValue("Bit1", out var i1) && i1 is JObject i1Obj 
			? InputBitDTO.FromJson(i1Obj) 
			: null;

		return new InputPortDTO(ReadOffset(json), bit1!);
	}
}
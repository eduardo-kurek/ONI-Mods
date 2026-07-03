using System.Text;
using CircuitNotIncluded.Core.Model;
using CircuitNotIncluded.Interfaces;
using CircuitNotIncluded.UI.Cells;
using KSerialization;
using Newtonsoft.Json.Linq;

namespace CircuitNotIncluded.Core.DTO;

public record RibbonOutputDTO (
	CellOffset Offset,
	[property: Serialize] OutputBitDTO Bit1,
	[property: Serialize] OutputBitDTO Bit2,
	[property: Serialize] OutputBitDTO Bit3,
	[property: Serialize] OutputBitDTO Bit4
) : PortDTO(Offset) {
	public override PortCategory Category => PortCategory.Output;
	
	public override string GetDisplayText(){
		StringBuilder sb = new();
		sb.Append(Bit1.GetDisplayText());
		sb.Append(Bit2.GetDisplayText());
		sb.Append(Bit3.GetDisplayText());
		sb.Append(Bit4.GetDisplayText());
		return sb.ToString();
	}
	
	public override JObject ToJson() {
		var ribbonOutputsJson = new JObject {
			{ "Bit1", Bit1.ToJson() },
			{ "Bit2", Bit2.ToJson() },
			{ "Bit3", Bit3.ToJson() },
			{ "Bit4", Bit4.ToJson() }
		};
		
		var portJson = base.ToJson();
		portJson.Merge(ribbonOutputsJson);
		return portJson;
	}

	public static RibbonOutputDTO FromJson(JObject json) {
		var bit1 = json.TryGetValue("Bit1", out var o1) && o1 is JObject o1Obj 
			? OutputBitDTO.FromJson(o1Obj) 
			: null;

		var bit2 = json.TryGetValue("Bit2", out var o2) && o2 is JObject o2Obj 
			? OutputBitDTO.FromJson(o2Obj) 
			: null;
		
		var bit3 = json.TryGetValue("Bit3", out var o3) && o3 is JObject o3Obj 
			? OutputBitDTO.FromJson(o3Obj) 
			: null;
		
		var bit4 = json.TryGetValue("Bit4", out var o4) && o4 is JObject o4Obj 
			? OutputBitDTO.FromJson(o4Obj) 
			: null;
		
		return new RibbonOutputDTO(ReadOffset(json), bit1!, bit2!, bit3!, bit4!);
	}

	public override void OnHover(string circuitName, HoverTextDrawer drawer, SelectToolHoverTextCard cfg){
		drawer.DrawText($"RIBBON OUTPUT    <style=\"hovercard_element\">({circuitName.ToUpper()})</style>", cfg.Styles_Title.Standard);
		drawer.NewLine();	
		drawer.DrawText($"Bit 1:", cfg.Styles_Title.Standard);
		drawer.NewLine();
		Bit1.OnHover(drawer, cfg);
		drawer.NewLine();
		drawer.DrawText($"Bit 2:", cfg.Styles_Title.Standard);
		drawer.NewLine();
		Bit2.OnHover(drawer, cfg);
		drawer.NewLine();
		drawer.DrawText($"Bit 3:", cfg.Styles_Title.Standard);
		drawer.NewLine();
		Bit3.OnHover(drawer, cfg);
		drawer.NewLine();
		drawer.DrawText($"Bit 4:", cfg.Styles_Title.Standard);
		drawer.NewLine();
		Bit4.OnHover(drawer, cfg);
	}

	public override IModel CreateModel(CircuitModel parent, OffsetResolver resolver)
		=> new RibbonOutputModel(this, parent, resolver);

	public override CircuitCellState CreateCellState()
		=> new RibbonOutputCellState(this);

}
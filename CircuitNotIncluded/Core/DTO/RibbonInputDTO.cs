using System.Text;
using CircuitNotIncluded.Core.Model;
using CircuitNotIncluded.Interfaces;
using CircuitNotIncluded.UI.Cells;
using KSerialization;
using Newtonsoft.Json.Linq;

namespace CircuitNotIncluded.Core.DTO;

public record RibbonInputDTO (
	CellOffset Offset,
	[property: Serialize] InputBitDTO Bit1,
	[property: Serialize] InputBitDTO Bit2,
	[property: Serialize] InputBitDTO Bit3,
	[property: Serialize] InputBitDTO Bit4
) : PortDTO(Offset) {
	public override PortCategory Category => PortCategory.Input;
	
	public override string GetDisplayText(){
		StringBuilder sb = new();
		sb.Append(Bit1.GetDisplayText());
		sb.Append(Bit2.GetDisplayText());
		sb.Append(Bit3.GetDisplayText());
		sb.Append(Bit4.GetDisplayText());
		return sb.ToString();
	}
	
	public override JObject ToJson() {
		var ribbonInputsJson = new JObject {
			{ "Bit1", Bit1.ToJson() },
			{ "Bit2", Bit2.ToJson() },
			{ "Bit3", Bit3.ToJson() },
			{ "Bit4", Bit4.ToJson() }
		};
		
		var portJson = base.ToJson();
		portJson.Merge(ribbonInputsJson);
		return portJson;
	}

	public static RibbonInputDTO FromJson(JObject json) {
		var bit1 = json.TryGetValue("Bit1", out var i1) && i1 is JObject i1Obj 
			? InputBitDTO.FromJson(i1Obj) 
			: null;

		var bit2 = json.TryGetValue("Bit2", out var i2) && i2 is JObject i2Obj 
			? InputBitDTO.FromJson(i2Obj) 
			: null;
		
		var bit3 = json.TryGetValue("Bit3", out var i3) && i3 is JObject i3Obj 
			? InputBitDTO.FromJson(i3Obj) 
			: null;
		
		var bit4 = json.TryGetValue("Bit4", out var i4) && i4 is JObject i4Obj 
			? InputBitDTO.FromJson(i4Obj) 
			: null;
		
		return new RibbonInputDTO(ReadOffset(json), bit1!, bit2!, bit3!, bit4!);
	}

	public override void OnHover(string circuitName, HoverTextDrawer drawer, SelectToolHoverTextCard cfg){
		drawer.DrawText($"RIBBON INPUT    <style=\"hovercard_element\">({circuitName.ToUpper()})</style>", cfg.Styles_Title.Standard);
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
		=> new RibbonInputModel(this, parent, resolver);

	public override CircuitCellState CreateCellState()
		=> new RibbonInputCellState(this);

}
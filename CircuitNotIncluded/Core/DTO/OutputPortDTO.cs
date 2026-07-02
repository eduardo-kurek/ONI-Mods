using System.Text;
using CircuitNotIncluded.Core.Model;
using CircuitNotIncluded.Interfaces;
using CircuitNotIncluded.UI.Cells;
using KSerialization;
using Newtonsoft.Json.Linq;

namespace CircuitNotIncluded.Core.DTO;

[SerializationConfig(MemberSerialization.OptIn)]
public record OutputPortDTO (
	CellOffset Offset,
	[property: Serialize] OutputBitDTO Bit1
) : PortDTO(Offset) {
	public override PortCategory Category => PortCategory.Output;

	public override string GetDisplayText(){
		StringBuilder sb = new();
		sb.AppendLine($"{Bit1.Label} = {Bit1.Expression}");
		if(Bit1.Description.Length > 0)
			sb.AppendLine($"• {Bit1.Description}");
		return sb.ToString();
	}

	public override JObject ToJson() {
		var outputJson = new JObject {
			{ "Bit1", Bit1.ToJson() }
		};

		var portJson = base.ToJson();
		portJson.Merge(outputJson);
		return portJson;
	}

	public static OutputPortDTO FromJson(JObject json) {
		var bit1 = json.TryGetValue("Bit1", out var o1) && o1 is JObject o1Obj 
			? OutputBitDTO.FromJson(o1Obj) 
			: null;

		return new OutputPortDTO(ReadOffset(json), bit1!);
	}
	
	public override void OnHover(string circuitName, HoverTextDrawer drawer, SelectToolHoverTextCard cfg) {
		drawer.DrawText($"OUTPUT  {Bit1.Label}    <style=\"hovercard_element\">({circuitName.ToUpper()})</style>", cfg.Styles_Title.Standard);
		drawer.NewLine();
		drawer.DrawText($"Expression = {Utils.UI.ColorizeExpression(Bit1.Expression)}", cfg.Styles_LogicActive.Standard);
		if (Bit1.Description.IsNullOrWhiteSpace()) return;
		drawer.NewLine();
		drawer.DrawIcon(cfg.iconDash);
		drawer.DrawText($"{Bit1.Description}", cfg.Styles_BodyText.Standard);
	}

	public override IModel CreateModel(CircuitModel parent, OffsetResolver resolver){
		return new OutputPortModel(this, parent, resolver);
	}

	public override CircuitCellState CreateCellState(){
		return new OutputCellState(Bit1);
	}
}
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
		return Bit1.GetDisplayText();
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
		Bit1.OnHover(drawer, cfg);
	}

	public override IModel CreateModel(CircuitModel parent, OffsetResolver resolver){
		return new OutputPortModel(this, parent, resolver);
	}

	public override CircuitCellState CreateCellState(){
		return new OutputCellState(Bit1);
	}
}
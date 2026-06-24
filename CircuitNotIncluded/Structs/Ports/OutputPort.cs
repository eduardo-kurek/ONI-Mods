using CircuitNotIncluded.Interfaces;
using KSerialization;
using Newtonsoft.Json.Linq;

namespace CircuitNotIncluded.Structs.Ports;

[SerializationConfig(MemberSerialization.OptIn)]
public record OutputPort (
	[property: Serialize] CellOffset Offset,
	[property: Serialize] OutputBit Bit1
) : IBlueprintSerializable, IHover 
{
	public void OnHover(string circuitName, HoverTextDrawer drawer, SelectToolHoverTextCard cfg) {
		drawer.DrawText($"OUTPUT  {Bit1.Label}    <style=\"hovercard_element\">({circuitName.ToUpper()})</style>", cfg.Styles_Title.Standard);
		drawer.NewLine();
		drawer.DrawText($"Expression = {Utils.UI.ColorizeExpression(Bit1.Expression)}", cfg.Styles_LogicActive.Standard);
		if (Bit1.Description.IsNullOrWhiteSpace()) return;
		drawer.NewLine();
		drawer.DrawIcon(cfg.iconDash);
		drawer.DrawText($"{Bit1.Description}", cfg.Styles_BodyText.Standard);
	}
   
	public JObject ToJson() {
		return new JObject {
			{ "Offset", JObject.FromObject(Offset) },
			{ "Bit1", Bit1.ToJson() }
		};
	}

	public static OutputPort FromJson(JObject json) {
		var offset = json["Offset"]?.ToObject<CellOffset>() ?? default;
		var bit1 = json.TryGetValue("Bit1", out var o1) && o1 is JObject o1Obj 
			? OutputBit.FromJson(o1Obj) 
			: null;
		return new OutputPort(offset, bit1!);
	}
}
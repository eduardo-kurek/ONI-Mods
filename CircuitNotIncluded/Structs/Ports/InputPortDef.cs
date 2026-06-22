using CircuitNotIncluded.Interfaces;
using KSerialization;
using Newtonsoft.Json.Linq;

namespace CircuitNotIncluded.Structs.Ports;

[SerializationConfig(MemberSerialization.OptIn)]
public record InputPortDef (
	[property: Serialize] CellOffset Offset,
	[property: Serialize] InputBit Bit1
) : IBlueprintSerializable, IHover {
	
	public void OnHover(string circuitName, HoverTextDrawer drawer, SelectToolHoverTextCard cfg) {
		drawer.DrawText($"INPUT  {Bit1.Id}    <style=\"hovercard_element\">({circuitName.ToUpper()})</style>", cfg.Styles_Title.Standard);
		if(Bit1.Description.IsNullOrWhiteSpace()) return;
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

	public static InputPortDef FromJson(JObject json) {
		var offset = json["Offset"]?.ToObject<CellOffset>() ?? default;
		var bit1 = json.TryGetValue("Bit1", out var i1) && i1 is JObject i1Obj 
			? InputBit.FromJson(i1Obj) 
			: null;
		return new InputPortDef(offset, bit1!);
	}
}
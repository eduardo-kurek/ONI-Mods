using KSerialization;
using Newtonsoft.Json.Linq;

namespace CircuitNotIncluded.Structs;
using static LogicPorts;

[SerializationConfig(MemberSerialization.OptIn)]
public class InputPort : CNIPort {
	private InputPort() { }
	
	private InputPort(string id, Port wrappedPort) : base(id, wrappedPort) {}

	public InputPort(InputPort other) : base(other) {}
	
	public static InputPort Create(CellOffset offset, PortInfo info){
		var port = Port.InputPort(
			info.Id, offset, info.Description, info.ActiveDescription, info.InactiveDescription
		);
		return new InputPort(info.Id, port);
	}

	public static InputPort CreateFromJson(JObject json){
		PortInfo info = ExtractPortInfo(json);
		CellOffset offset = ExtractOffset(json);
		return Create(offset, info);
	}

	public override void OnHover(string circuitName, HoverTextDrawer drawer, SelectToolHoverTextCard cfg){
		drawer.DrawText($"INPUT  {OriginalId}    <style=\"hovercard_element\">({circuitName.ToUpper()})</style>", cfg.Styles_Title.Standard);
		if(!Description.IsNullOrWhiteSpace()){
			drawer.NewLine();
			drawer.DrawIcon(cfg.iconDash);
			drawer.DrawText($"{Description}", cfg.Styles_BodyText.Standard);	
		}
	}
}
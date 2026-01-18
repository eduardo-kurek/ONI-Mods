namespace CircuitNotIncluded.Structs;
using static LogicPorts;

public class InputPort : CNIPort {
	private InputPort(string id, Port wrappedPort) : base(id, wrappedPort) {}
	
	public static InputPort Create(PortInfo info){
		var port = Port.InputPort(
			info.Id, info.Offset, info.Description, info.ActiveDescription, info.InactiveDescription
		);
		return new InputPort(info.Id, port);
	}
}
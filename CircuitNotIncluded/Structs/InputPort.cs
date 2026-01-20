namespace CircuitNotIncluded.Structs;
using static LogicPorts;

public class InputPort : CNIPort {
	private InputPort(string id, CellOffset offset, Port wrappedPort) : base(id, wrappedPort) {}
	
	public static InputPort Create(CellOffset offset, PortInfo info){
		var port = Port.InputPort(
			info.Id, offset, info.Description, info.ActiveDescription, info.InactiveDescription
		);
		return new InputPort(info.Id, offset, port);
	}
}
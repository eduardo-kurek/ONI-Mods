using KSerialization;

namespace CircuitNotIncluded.Structs;
using static LogicPorts;

[SerializationConfig(MemberSerialization.OptIn)]
public class InputPort : CNIPort {
	private InputPort() { }
	
	private InputPort(string id, Port wrappedPort) : base(id, wrappedPort) {}
	
	public static InputPort Create(CellOffset offset, PortInfo info){
		var port = Port.InputPort(
			info.Id, offset, info.Description, info.ActiveDescription, info.InactiveDescription
		);
		return new InputPort(info.Id, port);
	}
}
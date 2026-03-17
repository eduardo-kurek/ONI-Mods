using KSerialization;
using static LogicPorts;

namespace CircuitNotIncluded.Structs;

/**
 * This class is a wrapper for the Port class, which is a part of the game's API.
 * Contains the original id (not hashed) of the port and the Port object itself.
 */
[SerializationConfig(MemberSerialization.OptIn)]
public class CNIPort {
	[Serialize] public Port WrappedPort;
	[Serialize] public string OriginalId;
	
	public HashedString HashedId => WrappedPort.id;
	public string Description => WrappedPort.description;
	public string ActiveDescription => WrappedPort.activeDescription;
	public string InactiveDescription => WrappedPort.inactiveDescription;
	public CellOffset Offset => WrappedPort.cellOffset;
	
	protected CNIPort(){ }
	
	protected CNIPort(string id, Port wrappedPort){
		OriginalId = id;
		WrappedPort = wrappedPort;
	}

	public PortInfo GetInfo(){
		return new PortInfo(
			OriginalId,
			WrappedPort.description,
			WrappedPort.activeDescription,
			WrappedPort.inactiveDescription
		);
	}
}
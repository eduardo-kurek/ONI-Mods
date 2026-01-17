using static LogicPorts;

namespace CircuitNotIncluded.Structs;

/**
 * This class is a wrapper for the Port class, which is a part of the game's API.
 * Contains the original id (not hashed) of the port and the Port object itself.
 */
public class CNIPort {
	private readonly Port P;
	public string OriginalId { get; private set; }
	public HashedString HashedId => P.id;
	
	public Port WrappedPort => P;
	
	protected CNIPort(string id, Port port){
		OriginalId = id;
		P = port;
	}

	public PortInfo GetInfo(){
		return new PortInfo(
			OriginalId,
			WrappedPort.cellOffset,
			WrappedPort.description,
			WrappedPort.activeDescription,
			WrappedPort.inactiveDescription
		);
	}
}
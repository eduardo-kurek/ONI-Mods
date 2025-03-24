using static LogicPorts;

namespace CircuitNotIncluded.Structs;

/**
 * This class is a wrapper for the Port class, which is a part of the game's API.
 * Contains the original id (not hashed) of the port and the Port object itself.
 */
public class CNIPort {
	public string OriginalId;
	public readonly Port P;
	
	public static implicit operator Port(CNIPort cniPort) => cniPort.P;

	private CNIPort(string id, Port port){
		OriginalId = id;
		P = port;
	}

	public static CNIPort InputPort(string id, CellOffset cell_offset, string description, string activeDescription, 
		string inactiveDescription, bool show_wire_missing_icon = false, bool display_custom_name = false){
		var port = Port.InputPort(id, cell_offset, description, activeDescription, inactiveDescription, show_wire_missing_icon, display_custom_name);
		return new CNIPort(id, port);
	}
	
	public static CNIPort OutputPort(string id, CellOffset cell_offset, string description, string activeDescription, 
		string inactiveDescription, bool show_wire_missing_icon = false, bool display_custom_name = false){
		var port = Port.OutputPort(id, cell_offset, description, activeDescription, inactiveDescription, show_wire_missing_icon, display_custom_name);
		return new CNIPort(id, port);
	}
}
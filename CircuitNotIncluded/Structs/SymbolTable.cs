namespace CircuitNotIncluded.Structs;

public class SymbolTable {
	private readonly LogicPorts ports;
	private readonly Dictionary<string, HashedString> hashes = new();

	public SymbolTable(LogicPorts ports, List<InputPort> inputPorts){
		this.ports = ports;
		foreach(InputPort port in inputPorts)
			hashes.Add(port.OriginalId, port.HashedId);
	}
	
	public int GetInputValue(string id){
		return ports.GetInputValue(hashes[id]);
	}
}
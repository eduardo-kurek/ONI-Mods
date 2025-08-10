namespace CircuitNotIncluded.Structs;

public class SymbolTable {
	private readonly LogicPorts ports;
	private readonly Dictionary<string, HashedString> hashes = new();

	public SymbolTable(LogicPorts ports, List<CNIPort> inputPorts){
		this.ports = ports;
		foreach(CNIPort port in inputPorts)
			hashes.Add(port.OriginalId, port.P.id);
	}
	
	public int GetInputValue(string id){
		return ports.GetInputValue(hashes[id]);
	}
	
	public void UpdateOutput(HashedString id, int value){
		ports.SendSignal(id, value);
	}
}
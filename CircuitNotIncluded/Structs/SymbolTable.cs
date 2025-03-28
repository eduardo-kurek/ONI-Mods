namespace CircuitNotIncluded.Structs;

public class SymbolTable {
	private readonly LogicPorts ports;
	private readonly Dictionary<string, HashedString> hashes = new();

	public SymbolTable(LogicPorts ports, CircuitDef circuitDef){
		this.ports = ports;
		foreach(var port in circuitDef.CNI_InputPorts)
			hashes.Add(port.OriginalId, port.P.id);
	}
	
	public int GetInputValue(string id){
		return ports.GetInputValue(hashes[id]);
	}
	
	public void UpdateOutput(HashedString id, int value){
		ports.SendSignal(id, value);
	}
}
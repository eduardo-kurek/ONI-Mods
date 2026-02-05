namespace CircuitNotIncluded.Structs;

public class SymbolTable {
	private readonly IInputSource source;
	private readonly Dictionary<string, HashedString> hashes = new();

	public SymbolTable(IInputSource source, List<InputPort> inputPorts){
		this.source = source;
		foreach(InputPort port in inputPorts)
			hashes.Add(port.OriginalId, port.HashedId);
	}
	
	public int GetInputValue(string id){
		return source.GetInputPortValue(hashes[id]);
	}
}
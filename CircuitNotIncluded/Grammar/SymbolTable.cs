namespace CircuitNotIncluded.Grammar;

public class SymbolTable {
	private readonly Dictionary<string, int> values = [];
	
	public int GetValue(string id) => values.TryGetValue(id, out int value) ? value : 0;
	public void SetValue(string id, int value) => values[id] = value;
}
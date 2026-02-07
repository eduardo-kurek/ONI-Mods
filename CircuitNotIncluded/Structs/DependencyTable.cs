namespace CircuitNotIncluded.Structs;

// Stores the dependencies between input ports and output ports
// In other words, which output ports are influenced by each input port
public class DependencyTable {
	private readonly Dictionary<string, List<CircuitOutput>> table = new();

	public void RegisterDependency(string inputId, CircuitOutput output) {
		if (!table.ContainsKey(inputId))
				table[inputId] = [];
		
		if (!table[inputId].Contains(output)) 
			table[inputId].Add(output);
	}

	public List<CircuitOutput> GetOutputDependents(string inputId) {
		return table.TryGetValue(inputId, out var list) 
			? list : [];
	}

	public void Clear() => table.Clear();
}
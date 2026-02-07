using CircuitNotIncluded.Grammar;

namespace CircuitNotIncluded.Structs;

// Stores the dependencies between input ports and output ports
// In other words, which output ports are influenced by each input port
public class DependencyTable {
	private readonly Dictionary<HashedString, List<HashedString>> table = new();

	public void RegisterDependency(HashedString inputId, HashedString outputId) {
		if (!table.ContainsKey(inputId)) table[inputId] = new List<HashedString>();
		if (!table[inputId].Contains(outputId)) table[inputId].Add(outputId);
	}

	public List<HashedString> GetOutputDependents(HashedString inputId) {
		return table.TryGetValue(inputId, out var list) ? list : new List<HashedString>();
	}

	public void Clear() => table.Clear();
}
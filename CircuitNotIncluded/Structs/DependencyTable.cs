using CircuitNotIncluded.Grammar;

namespace CircuitNotIncluded.Structs;

// Stores the dependencies between input ports and output ports
// In other words, which output ports are influenced by each input port
public class DependencyTable {

	private readonly Dictionary<HashedString, List<Output>> table = new();
	
	public DependencyTable(List<CNIPort> inputPorts, List<Output> outputPorts){
		InitializeEmptyTable(inputPorts);
		InitializeDependencies(outputPorts);
	}

	private void InitializeEmptyTable(List<CNIPort> inputPorts){
		foreach(CNIPort port in inputPorts){
			table[port.P.id] = [];
		}
	}

	private void InitializeDependencies(List<Output> outputs){
		foreach(Output output in outputs)
			AddDependency(output);
	}
	
	private void AddDependency(Output output){
		var inputPorts = IdExtractor.Extract(output.Tree);
		foreach(string inputPort in inputPorts){
			table[inputPort].Add(output);
		}
	}
	
	public List<Output> GetOutputDependents(HashedString inputPortId){
		return table[inputPortId];
	}

	public bool HasInputPort(HashedString inputPortId){
		return table.ContainsKey(inputPortId);
	}
}
using CircuitNotIncluded.Grammar;
using CircuitNotIncluded.Grammar.Visitors;

namespace CircuitNotIncluded.Structs;

// Stores the dependencies between input ports and output ports
// In other words, which output ports are influenced by each input port
public class DependencyTable {

	private readonly Dictionary<HashedString, List<OutputPort>> table = new();
	
	public DependencyTable(List<InputPort> inputPorts, List<OutputPort> outputPorts){
		InitializeEmptyTable(inputPorts);
		InitializeDependencies(outputPorts);
	}

	private void InitializeEmptyTable(List<InputPort> inputPorts){
		foreach(InputPort port in inputPorts){
			table[port.HashedId] = [];
		}
	}

	private void InitializeDependencies(List<OutputPort> outputs){
		foreach(OutputPort output in outputs)
			AddDependency(output);
	}
	
	private void AddDependency(OutputPort outputPort){
		var inputPorts = Compiler.ExtractIds(outputPort.Tree);
		foreach(string inputPort in inputPorts){
			table[inputPort].Add(outputPort);
		}
	}
	
	public List<OutputPort> GetOutputDependents(HashedString inputPortId){
		return table[inputPortId];
	}

	public bool HasInputPort(HashedString inputPortId){
		return table.ContainsKey(inputPortId);
	}
}
using CircuitNotIncluded.Grammar;

namespace CircuitNotIncluded.Structs;

// Stores the dependencies between input ports and output ports
// In other words, which output ports are influenced by each input port
public class DependencyTable {

	private readonly Dictionary<HashedString, List<Output>> table = new();
	
	public DependencyTable(CircuitDef def){
		Update(def);
	}

	private void Update(CircuitDef def){
		InitializeEmptyTable(def.CNI_InputPorts);
		InitializeDependecies(def.CNI_Outputs);
	}

	private void InitializeEmptyTable(List<CNIPort> inputPorts){
		foreach(CNIPort port in inputPorts){
			table[port.P.id] = [];
		}
	}

	private void InitializeDependecies(List<Output> outputs){
		foreach(Output output in outputs)
			AddDependecy(output);
	}
	
	private void AddDependecy(Output output){
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
using CircuitNotIncluded.Syntax;
using CircuitNotIncluded.Syntax.Visitors;

namespace CircuitNotIncluded.Structs;

// Stores the dependencies between input ports and output ports
// In other words, which output ports are influenced by each input port
public class DependencyTable {

	private readonly Dictionary<HashedString, List<SyntaxTree>> table = new();
	
	public DependencyTable(CircuitDef def){
		Update(def);
	}

	private void Update(CircuitDef def){
		InitializeEmptyTable(def.CNI_InputPorts);
		InitializeDependecies(def.CNI_Outputs);
	}

	private void InitializeEmptyTable(List<CNIPort> inputPorts){
		foreach(CNIPort port in inputPorts){
			table[port.P.id] = new List<SyntaxTree>();
		}
	}

	private void InitializeDependecies(List<SyntaxTree> outputs){
		foreach(SyntaxTree tree in outputs)
			AddDependecy(tree);
	}
	
	private void AddDependecy(SyntaxTree tree){
		var inputPorts = ExtractInputPorts(tree);
		foreach(HashedString inputPort in inputPorts){
			table[inputPort].Add(tree);
		}
	}
	
	public List<SyntaxTree> GetDependents(HashedString inputPortId){
		return table[inputPortId];
	}

	public bool HasInputPort(HashedString inputPortId){
		return table.ContainsKey(inputPortId);
	}
	
	private static List<HashedString> ExtractInputPorts(SyntaxTree tree){
		InputPortsExtractor extractor = new();
		tree.Accept(extractor);
		return extractor.InputPorts;
	}
}
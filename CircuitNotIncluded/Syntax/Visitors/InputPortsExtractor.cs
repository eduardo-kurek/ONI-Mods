using CircuitNotIncluded.Syntax.Nodes;

namespace CircuitNotIncluded.Syntax.Visitors;

public class InputPortsExtractor : IVisitor {
	public void Visit(Identifier node){
		InputPorts.Add(node.InputPortId);
	}
	
	public void Visit(BinaryOperation node){
		node.Left.Accept(this);
		node.Right.Accept(this);
	}
	
	public void Visit(UnaryOperation node){
		node.Child.Accept(this);
	}
	
	public List<HashedString> InputPorts { get; } = new();
}
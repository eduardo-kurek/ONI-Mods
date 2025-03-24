using CircuitNotIncluded.Syntax.Nodes;
using UnityEngine.Windows;

namespace CircuitNotIncluded.Syntax.Visitors;

public class InputPortsExtractor : IVisitor {
	private readonly List<HashedString> InputPorts = new();
	
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

	public void Clear() => InputPorts.Clear();

	public static List<HashedString> Extract(SyntaxTree tree){
		InputPortsExtractor extractor = new();
		tree.Accept(extractor);
		return extractor.InputPorts;
	}
}
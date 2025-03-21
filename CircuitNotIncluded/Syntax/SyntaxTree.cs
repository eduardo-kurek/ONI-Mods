using CircuitNotIncluded.Syntax.Nodes;
using CircuitNotIncluded.Syntax.Visitors;

namespace CircuitNotIncluded.Syntax;

public class SyntaxTree {
	public LogicPorts.Port OutputPort { get; private set; }
	private INode Root;

	public SyntaxTree(LogicPorts.Port outputPort, INode root){
		OutputPort = outputPort;
		Root = root;
	}

	public void Accept(IVisitor v){
		Root.Accept(v);
	}
	
}
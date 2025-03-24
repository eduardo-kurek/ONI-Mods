using CircuitNotIncluded.Structs;
using CircuitNotIncluded.Syntax.Nodes;
using CircuitNotIncluded.Syntax.Visitors;

namespace CircuitNotIncluded.Syntax;

public class SyntaxTree {
	public CNIPort OutputPort { get; private set; }
	private readonly INode Root;

	public SyntaxTree(CNIPort outputPort, INode root){
		OutputPort = outputPort;
		Root = root;
	}

	public void Accept(IVisitor v){
		Root.Accept(v);
	}
	
	public string GetOutputId() => OutputPort.OriginalId;
	public HashedString GetOutputHash() => OutputPort.P.id;
}
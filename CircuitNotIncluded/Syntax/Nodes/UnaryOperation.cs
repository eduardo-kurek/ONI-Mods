using CircuitNotIncluded.Syntax.Kinds;
using CircuitNotIncluded.Syntax.Visitors;

namespace CircuitNotIncluded.Syntax.Nodes;

public class UnaryOperation : INode {
	
	public INode Child;
	private readonly IUnaryKind kind;
	
	public UnaryOperation(INode child, IUnaryKind kind){
		Child = child;
		this.kind = kind;
	}
	
	public void Accept(IVisitor v){
		v.Visit(this);
	}
	
	public int Evaluate(int value){
		return kind.Evaluate(value);
	}
}
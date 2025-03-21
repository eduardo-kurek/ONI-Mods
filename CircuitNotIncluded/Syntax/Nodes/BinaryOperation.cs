using CircuitNotIncluded.Syntax.Kinds;
using CircuitNotIncluded.Syntax.Visitors;

namespace CircuitNotIncluded.Syntax.Nodes;

public class BinaryOperation : INode {
	
	public INode Left;
	public INode Right;
	private IBinaryKind _kind;

	public BinaryOperation(INode left, INode right, IBinaryKind kind){
		Left = left;
		Right = right;
		_kind = kind;
	}

	public void Accept(IVisitor v){
		v.Visit(this);
	}
	
	public int Evaluate(int left, int right){
		return _kind.Evaluate(left, right);
	}
	
}
using CircuitNotIncluded.Syntax.Visitors;

namespace CircuitNotIncluded.Syntax.Nodes;

public interface INode {
	void Accept(IVisitor v);
}
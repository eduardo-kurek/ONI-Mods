using CircuitNotIncluded.Syntax.Nodes;

namespace CircuitNotIncluded.Syntax.Visitors;

public interface IVisitor {
	void Visit(Identifier node);
	void Visit(BinaryOperation node);
	void Visit(UnaryOperation node);
}
using CircuitNotIncluded.Syntax.Nodes;

namespace CircuitNotIncluded.Syntax.Visitors;

public class SyntaxEvaluater : IVisitor {
	
	private readonly LogicPorts ports;
	private int value;

	public SyntaxEvaluater(LogicPorts ports){
		this.ports = ports;
		value = 0;
	}
	
	public void Visit(Identifier node){
		value = ports.GetInputValue(node.InputPortId);
	}
	
	public void Visit(BinaryOperation node){
		node.Left.Accept(this);
		int lValue = value;
		
		node.Right.Accept(this);
		int rValue = value;

		value = node.Evaluate(lValue, rValue);
	}
	
	public void Visit(UnaryOperation node){
		node.Child.Accept(this);
		int value = this.value;

		this.value = node.Evaluate(value);
	}

	public int GetResult(){
		return value;
	}

}
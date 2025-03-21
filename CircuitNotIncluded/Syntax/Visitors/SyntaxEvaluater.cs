using CircuitNotIncluded.Syntax.Nodes;

namespace CircuitNotIncluded.Syntax.Visitors;

public class SyntaxEvaluater : IVisitor {
	
	private readonly LogicPorts _ports;
	private int _value;

	public SyntaxEvaluater(LogicPorts ports){
		_ports = ports;
		_value = 0;
	}
	
	public void Visit(Identifier node){
		_value = _ports.GetInputValue(node.InputPortId);
	}
	
	public void Visit(BinaryOperation node){
		node.Left.Accept(this);
		int lValue = _value;
		
		node.Right.Accept(this);
		int rValue = _value;

		_value = node.Evaluate(lValue, rValue);
	}
	
	public void Visit(UnaryOperation node){
		node.Child.Accept(this);
		int value = _value;

		_value = node.Evaluate(value);
	}

	public int GetResult(){
		return _value;
	}

}
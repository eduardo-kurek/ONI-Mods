using CircuitNotIncluded.Syntax.Visitors;

namespace CircuitNotIncluded.Syntax.Nodes;

public class Identifier : INode {
	public HashedString InputPortId { get; }

	public Identifier(LogicPorts.Port inputPort){
		InputPortId = inputPort.id;
	}
	
	public void Accept(IVisitor v){
		v.Visit(this);
	}
}
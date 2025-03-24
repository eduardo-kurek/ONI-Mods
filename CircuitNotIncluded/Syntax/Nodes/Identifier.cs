using CircuitNotIncluded.Structs;
using CircuitNotIncluded.Syntax.Visitors;

namespace CircuitNotIncluded.Syntax.Nodes;

public class Identifier : INode {
	public string OriginalId { get; }
	public HashedString InputPortId { get; }

	public Identifier(CNIPort inputPort){
		OriginalId = inputPort.OriginalId;
		InputPortId = inputPort.P.id;
	}
	
	public void Accept(IVisitor v){
		v.Visit(this);
	}
}
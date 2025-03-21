namespace CircuitNotIncluded.Syntax.Kinds;

public class NotKind : IUnaryKind {
	public int Evaluate(int value){
		return ~value;
	}
}
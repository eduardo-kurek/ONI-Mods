namespace CircuitNotIncluded.Syntax.Kinds;

public class AndKind : IBinaryKind {
	public int Evaluate(int left, int right){
		return left & right;
	}
}
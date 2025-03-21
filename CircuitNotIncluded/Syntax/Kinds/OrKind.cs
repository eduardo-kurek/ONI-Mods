namespace CircuitNotIncluded.Syntax.Kinds;

public class OrKind : IBinaryKind {
	public int Evaluate(int left, int right){
		return left | right;
	}
}
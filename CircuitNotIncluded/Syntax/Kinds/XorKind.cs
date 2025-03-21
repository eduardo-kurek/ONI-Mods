using CircuitNotIncluded.Syntax.Kinds;

namespace CircuitNotIncluded.Syntax.Binary;

public class XorKind : IBinaryKind {
	public int Evaluate(int left, int right){
		return left ^ right;
	}
}
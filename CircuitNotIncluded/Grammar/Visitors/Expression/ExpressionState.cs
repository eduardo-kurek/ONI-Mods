namespace CircuitNotIncluded.Grammar.Visitors.Expression;

public class ExpressionState(IStatefulGate[] gates) {
	public int EvaluateGate(int currentInput, int index){
		return gates[index].Evaluate(currentInput);
	}
}
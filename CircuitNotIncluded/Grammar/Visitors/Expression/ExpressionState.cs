namespace CircuitNotIncluded.Grammar.Visitors.Expression;

public class ExpressionState(TransitionDelayState[] delayStates) {
	public int EvaluateDelayGate(int currentInput, int index){
		return delayStates[index].Evaluate(currentInput);
	}
}
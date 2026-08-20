namespace CircuitNotIncluded.Grammar.Visitors.Expression;

public interface IStatefulGate {
	int Evaluate(int currentInput);
}
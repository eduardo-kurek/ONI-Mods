namespace CircuitNotIncluded.Grammar;

using EvaluateFunc = Func<SymbolTable, ExpressionState, int>;

public class CompiledExpression(EvaluateFunc evaluateFunc, ExpressionState state) {
	public int Evaluate(SymbolTable symbolTable){
		return evaluateFunc.Invoke(symbolTable, state);
	}
}
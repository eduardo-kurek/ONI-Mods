using CircuitNotIncluded.Core.DTO;
using CircuitNotIncluded.Grammar;
using CircuitNotIncluded.Grammar.Visitors.Expression;

namespace CircuitNotIncluded.Core.Model;

public class OutputBitModel(OutputBitDTO bit, PortModel port, int bitNumber) {
	public string Label { get; } = bit.Label;
	public string Description { get; } = bit.Description;
	public string Expression { get; } = bit.Expression;
	public int BitNumber { get; } = bitNumber;
	public PortModel Port { get; } = port;

	public CompiledExpression Compile(Dictionary<string, ExpressionState> statesByLabel){
		if (statesByLabel.TryGetValue(Label, out ExpressionState state)) {
			return Compiler.Compile(Expression, state);
		}

		CompiledExpression compiledExp = Compiler.Compile(Expression);
		statesByLabel[Label] = compiledExp.State;
		return compiledExp;
	}
}
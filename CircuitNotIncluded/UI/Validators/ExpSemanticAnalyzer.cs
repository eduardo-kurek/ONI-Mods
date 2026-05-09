using CircuitNotIncluded.Grammar;
using CircuitNotIncluded.UI.Cells;
using static CircuitNotIncluded.Grammar.ExpressionParser;

namespace CircuitNotIncluded.UI.Validators;

public class ExpSemanticValidator : BasePortValidator<OutputCellState> {
	private string error = "";

	protected override bool DispatchErrorWhen(OutputCellState cell, ValidationContext ctx){
		ProgramContext tree = ctx.Parse(cell);
		try {
			Compiler.SemanticAnalyze(tree, ctx.GetInputIds());
			return false;
		}
		catch(Exception e) {
			error = e.Message;
			return true;
		}
	}
	protected override string GetErrorMessage(OutputCellState cell, ValidationContext ctx){
		return error;
	}
}
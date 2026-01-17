using CircuitNotIncluded.Grammar;
using CircuitNotIncluded.UI.Cells;
using static CircuitNotIncluded.Grammar.ExpressionParser;

namespace CircuitNotIncluded.UI.Validators;

public class ExpSemanticAnalyzer(PortHandler? next = null) : PortHandler(next) {
	private string error = "";
	protected override bool CanHandle(PortCellState cell, ValidationContext ctx){
		return cell is OutputCellState;
	}
	protected override bool ErrorOccurred(PortCellState cell, ValidationContext ctx){
		var outputCell = (OutputCellState)cell;
		ProgramContext tree = ctx.Parse(outputCell);
		try {
			Compiler.SemanticAnalyze(tree, ctx.GetInputIds());
			return false;
		}
		catch(Exception e) {
			error = e.Message;
			return true;
		}
	}
	protected override string GetErrorMessage(PortCellState cell, ValidationContext ctx){
		return error;
	}
}
using CircuitNotIncluded.Grammar;
using CircuitNotIncluded.UI.Cells;
using static CircuitNotIncluded.Grammar.ExpressionParser;

namespace CircuitNotIncluded.UI.Validators;

public class ExpSemanticAnalyzer(PortHandler? next = null) : PortHandler(next) {
	private string error = "";
	protected override bool CanHandle(PortCellType cell, ValidationContext ctx){
		return cell is OutputCellType;
	}
	protected override bool ErrorOccurred(PortCellType cell, ValidationContext ctx){
		var outputCell = (OutputCellType)cell;
		ProgramContext tree = ctx.Parse(outputCell);
		try{
			SemanticAnalyzer.Analyze(tree, ctx.GetInputIds());
			return false;
		}
		catch(Exception e){
			error = e.Message;
			return true;
		}
	}
	protected override string GetErrorMessage(PortCellType cell, ValidationContext ctx){
		return error;
	}
}
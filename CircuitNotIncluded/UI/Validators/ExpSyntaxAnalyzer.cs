using CircuitNotIncluded.Structs;
using CircuitNotIncluded.UI.Cells;
using static CircuitNotIncluded.Grammar.ExpressionParser;

namespace CircuitNotIncluded.UI.Validators;

public class ExpSyntaxAnalyzer(PortHandler? next = null) : PortHandler(next) {
	private string error = "";
	protected override bool CanHandle(PortCellState cell, ValidationContext ctx){
		return cell is OutputCellState;
	}
	protected override bool ErrorOccurred(PortCellState cell, ValidationContext ctx){
		var outputCell = (OutputCellState)cell;
		try{
			ctx.Parse(outputCell);
			return false;
		}
		catch(Exception e){
			error = e.Message;
			return true;
		}
	}
	protected override string GetErrorMessage(PortCellState cell, ValidationContext ctx){
		return error;
	}
}
using CircuitNotIncluded.Structs;
using CircuitNotIncluded.UI.Cells;
using static CircuitNotIncluded.Grammar.ExpressionParser;

namespace CircuitNotIncluded.UI.Validators;

public class ExpSyntaxAnalyzer(PortHandler? next = null) : PortHandler(next) {
	private string error = "";
	protected override bool CanHandle(PortCellType cell, ValidationContext ctx){
		return cell is OutputCellType;
	}
	protected override bool ErrorOccurred(PortCellType cell, ValidationContext ctx){
		var outputCell = (OutputCellType)cell;
		try{
			ctx.Parse(outputCell);
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
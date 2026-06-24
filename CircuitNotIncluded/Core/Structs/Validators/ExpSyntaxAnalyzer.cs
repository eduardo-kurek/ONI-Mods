using CircuitNotIncluded.UI.Cells;

namespace CircuitNotIncluded.Core.Structs.Validators;

public class ExpSyntaxValidator : BasePortValidator<PortCellState> {
	private string error = "";

	protected override bool DispatchErrorWhen(PortCellState cell, ValidationContext ctx){
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
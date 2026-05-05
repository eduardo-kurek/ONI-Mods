using CircuitNotIncluded.UI.Cells;

namespace CircuitNotIncluded.UI.Validators;

public class ExpNotEmptyValidator : BasePortValidator<OutputCellState> {
	protected override bool DispatchErrorWhen(OutputCellState cell, ValidationContext ctx){
		return cell.GetExpression().IsNullOrWhiteSpace();
	}
	protected override string GetErrorMessage(OutputCellState cell, ValidationContext ctx){
		return "Expression cannot be empty.";
	}
}
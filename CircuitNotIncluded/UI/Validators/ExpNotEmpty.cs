using CircuitNotIncluded.UI.Cells;

namespace CircuitNotIncluded.UI.Validators;

public class ExpNotEmpty(PortHandler? next = null) : PortHandler(next) {
	protected override bool CanHandle(PortCellState cell, ValidationContext ctx){
		return cell is OutputCellState;
	}
	protected override bool ErrorOccurred(PortCellState cell, ValidationContext ctx){
		return ((OutputCellState)cell).GetExpression().IsNullOrWhiteSpace();
	}
	protected override string GetErrorMessage(PortCellState cell, ValidationContext ctx){
		return "Expression cannot be empty.";
	}
}
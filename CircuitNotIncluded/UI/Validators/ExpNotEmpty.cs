using CircuitNotIncluded.UI.Cells;

namespace CircuitNotIncluded.UI.Validators;

public class ExpNotEmpty(PortHandler? next = null) : PortHandler(next) {
	protected override bool CanHandle(PortCellType cell, ValidationContext ctx){
		return cell is OutputCellType;
	}
	protected override bool ErrorOccurred(PortCellType cell, ValidationContext ctx){
		return ((OutputCellType)cell).GetExpression().IsNullOrWhiteSpace();
	}
	protected override string GetErrorMessage(PortCellType cell, ValidationContext ctx){
		return "Expression cannot be empty.";
	}
}
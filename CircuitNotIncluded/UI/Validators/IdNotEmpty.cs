using CircuitNotIncluded.UI.Cells;

namespace CircuitNotIncluded.UI.Validators;

public class IdNotEmpty(PortHandler? next = null) : PortHandler(next) {
	protected override bool ErrorOccurred(PortCellType cell, ValidationContext ctx){
		return cell.GetId().IsNullOrWhiteSpace();
	}
	protected override string GetErrorMessage(PortCellType cell, ValidationContext ctx){
		return "Port id cannot be empty.";
	}
}
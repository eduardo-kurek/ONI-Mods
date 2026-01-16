using CircuitNotIncluded.UI.Cells;

namespace CircuitNotIncluded.UI.Validators;

public class IdNotContainSpaces(PortHandler? next = null) : PortHandler(next) {
	protected override bool ErrorOccurred(PortCellState cell, ValidationContext ctx){
		return cell.GetId().Contains(" ");
	}
	protected override string GetErrorMessage(PortCellState cell, ValidationContext ctx){
		return "Port cannot contain spaces.";
	}
}
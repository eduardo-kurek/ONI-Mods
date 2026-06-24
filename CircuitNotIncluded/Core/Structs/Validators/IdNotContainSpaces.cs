using CircuitNotIncluded.UI.Cells;

namespace CircuitNotIncluded.Core.Structs.Validators;

public class IdNotContainSpacesValidator : BasePortValidator<PortCellState> {
	protected override bool DispatchErrorWhen(PortCellState cell, ValidationContext ctx){
		return cell.GetId().Contains(" ");
	}
	
	protected override string GetErrorMessage(PortCellState cell, ValidationContext ctx){
		return "Port cannot contain spaces.";
	}
}
using CircuitNotIncluded.UI.Cells;

namespace CircuitNotIncluded.Core.Structs.Validators;

public class IdNotEmptyValidator : BasePortValidator<PortCellState> {
	protected override bool DispatchErrorWhen(PortCellState port, ValidationContext ctx){
		return port.GetId().IsNullOrWhiteSpace();
	}

	protected override string GetErrorMessage(PortCellState port, ValidationContext ctx){
		return "Port id cannot be empty.";
	}
}
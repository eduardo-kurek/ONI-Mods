using CircuitNotIncluded.UI.Cells;

namespace CircuitNotIncluded.Core.Structs.Validators;

public class IdNotDuplicatedValidator : BasePortValidator<PortCellState> {
	protected override bool DispatchErrorWhen(PortCellState port, ValidationContext ctx)
		=> IsDuplicated(port, ctx);
	
	private static bool IsDuplicated(PortCellState cell, ValidationContext ctx)
		=> ctx.IsPortDeclared(cell);
	
	protected override void OnSuccess(PortCellState cell, ValidationContext ctx)
		=> ctx.DeclarePort(cell);

	protected override string GetErrorMessage(PortCellState port, ValidationContext ctx){
		var declaredPort = ctx.GetDeclaredPort(port.GetId());
		return $"Duplicated port id: {port.GetId()}. Already declared in ({declaredPort.Owner.DisplayIndex}).";
	}
}
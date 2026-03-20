using CircuitNotIncluded.UI.Cells;

namespace CircuitNotIncluded.UI.Validators;

public class IdNotDuplicated(PortHandler? next = null) : PortHandler(next) {
	protected override bool ErrorOccurred(PortCellState cell, ValidationContext ctx) 
		=> IsDuplicated(cell, ctx);

	private static bool IsDuplicated(PortCellState cell, ValidationContext ctx)
		=> ctx.IsPortDeclared(cell);

	protected override void OnSuccess(PortCellState cell, ValidationContext ctx)
		=> ctx.DeclarePort(cell);
	
	protected override string GetErrorMessage(PortCellState cell, ValidationContext ctx){
		var declaredPort = ctx.GetDeclaredPort(cell.Id);
		return $"Duplicated port id: {cell.Id}. Already declared in ({declaredPort.Owner.DisplayIndex}).";
	}
}
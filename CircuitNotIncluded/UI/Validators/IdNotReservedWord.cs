using CircuitNotIncluded.UI.Cells;
using static CircuitNotIncluded.Grammar.ExpressionUtils;

namespace CircuitNotIncluded.UI.Validators;

public class IdNotReservedWord : BasePortValidator<PortCellState> {
	protected override bool DispatchErrorWhen(PortCellState port, ValidationContext ctx)
		=> ReservedWords.Contains(port.GetId());
	
	protected override string GetErrorMessage(PortCellState port, ValidationContext ctx){
		return $"Id '{port.GetId()}' is a reserved word";
	}
}
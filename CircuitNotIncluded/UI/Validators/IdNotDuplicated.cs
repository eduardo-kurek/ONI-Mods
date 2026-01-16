using CircuitNotIncluded.UI.Cells;

namespace CircuitNotIncluded.UI.Validators;

public class IdNotDuplicated(PortHandler? next = null) : PortHandler(next) {
	protected override bool ErrorOccurred(PortCellState cell, ValidationContext ctx) 
		=> IsDuplicated(cell, ctx);
	
	private static bool IsDuplicated(PortCellState cell, ValidationContext ctx) 
		=> ctx.DeclaredIds.ContainsKey(cell.GetId());
	
	protected override void OnSuccess(PortCellState cell, ValidationContext ctx) 
		=> Register(cell, ctx);
	
	private static void Register(PortCellState cell, ValidationContext ctx){
		ctx.DeclaredIds[cell.GetId()] = cell.GetOffset();
	}
	
	protected override string GetErrorMessage(PortCellState cell, ValidationContext ctx){
		string id = cell.GetId();
		CellOffset offset = ctx.DeclaredIds[id];
		return $"Duplicated port id: {id}. Already declared in ({offset.x}, {offset.y}).";
	}
}
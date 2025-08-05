using CircuitNotIncluded.UI.Cells;

namespace CircuitNotIncluded.UI.Validators;

public class IdNotDuplicated(PortHandler? next = null) : PortHandler(next) {
	protected override bool ErrorOccurred(PortCellType cell, ValidationContext ctx) 
		=> IsDuplicated(cell, ctx);
	
	private static bool IsDuplicated(PortCellType cell, ValidationContext ctx) 
		=> ctx.DeclaredIds.ContainsKey(cell.GetId());
	
	protected override void OnSuccess(PortCellType cell, ValidationContext ctx) 
		=> Register(cell, ctx);
	
	private static void Register(PortCellType cell, ValidationContext ctx){
		ctx.DeclaredIds[cell.GetId()] = cell.GetOffset();
	}
	
	protected override string GetErrorMessage(PortCellType cell, ValidationContext ctx){
		string id = cell.GetId();
		CellOffset offset = ctx.DeclaredIds[id];
		return $"Duplicated port id: {id}. Already declared in ({offset.x}, {offset.y}).";
	}
}
using CircuitNotIncluded.Grammar;
using CircuitNotIncluded.UI.Cells;
using static CircuitNotIncluded.Grammar.ExpressionParser;

namespace CircuitNotIncluded.UI.Validators;

public class ExpHasValidIds(PortHandler? next = null) : PortHandler(next) {
	private readonly HashSet<string> invalidIds = [];

	protected override void Clear(PortCellState cell, ValidationContext ctx){
		invalidIds.Clear();
	}

	protected override bool ErrorOccurred(PortCellState cell, ValidationContext ctx){
		return cell is OutputCellState output 
		       && HasInvalidIds(output, ctx);
	}
	
	private bool HasInvalidIds(OutputCellState output, ValidationContext ctx){
		ProgramContext tree = ctx.Parse(output);
		var ids = IdExtractor.Extract(tree);
		foreach(string id in ids.Where(ctx.HasOutputId))
			AddInvalidId(id);
		return invalidIds.Any();
	}
	
	private void AddInvalidId(string id){
		invalidIds.Add(id);
	}

	protected override string GetErrorMessage(PortCellState cell, ValidationContext ctx){
		return $"Only input port ids can be used in expressions ({string.Join(", ", invalidIds)}) ";
	}
}
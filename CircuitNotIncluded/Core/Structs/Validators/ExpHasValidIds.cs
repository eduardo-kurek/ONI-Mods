using CircuitNotIncluded.Grammar;
using CircuitNotIncluded.UI.Cells;
using static CircuitNotIncluded.Grammar.ExpressionParser;

namespace CircuitNotIncluded.Core.Structs.Validators;

public class ExpHasValidIdsValidator : BasePortValidator<OutputCellState> {
	private readonly HashSet<string> invalidIds = [];

	protected override void BeforeEach(OutputCellState cell, ValidationContext ctx){
		invalidIds.Clear();
	}

	protected override bool DispatchErrorWhen(OutputCellState cell, ValidationContext ctx){
		return HasInvalidIds(cell, ctx);
	}
	
	private bool HasInvalidIds(OutputCellState output, ValidationContext ctx){
		ProgramContext tree = ctx.Parse(output);
		var ids = Compiler.ExtractIds(tree);
		foreach(string id in ids.Where(ctx.HasOutputId))
			AddInvalidId(id);
		return invalidIds.Any();
	}
	
	private void AddInvalidId(string id){
		invalidIds.Add(id);
	}

	protected override string GetErrorMessage(OutputCellState cell, ValidationContext ctx){
		return $"Only input port ids can be used in expressions ({string.Join(", ", invalidIds)}) ";
	}
}
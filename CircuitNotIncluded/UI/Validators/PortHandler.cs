using CircuitNotIncluded.UI.Cells;

namespace CircuitNotIncluded.UI.Validators;

public abstract class PortHandler(PortHandler? next) {
	private PortHandler? next = next;

	public void SetNext(PortHandler next){
		this.next = next;
	}

	public void Handle(PortCellState cell, ValidationContext ctx){
		Clear(cell, ctx);
		if(CanHandle(cell, ctx) && ErrorOccurred(cell, ctx)){
			OnError(cell, ctx);
			RegisterError(cell, ctx);
			return;
		}
		OnSuccess(cell, ctx);
		Next(cell, ctx);
	}

	private bool MustChainNext(PortCellState cell, ValidationContext ctx){
		if(CanHandle(cell, ctx)) return false;
		if(ErrorOccurred(cell, ctx)){
			OnError(cell, ctx);
			return false;
		}
		OnSuccess(cell, ctx);
		return true;
	}

	protected virtual bool CanHandle(PortCellState cell, ValidationContext ctx) => true;
	protected abstract bool ErrorOccurred(PortCellState cell, ValidationContext ctx);

	private void Next(PortCellState cell, ValidationContext ctx){
		next?.Handle(cell, ctx);
	}
	
	private void RegisterError(PortCellState cell, ValidationContext ctx){
		ctx.AddError(cell, GetErrorMessage(cell, ctx));
	}
	
	protected abstract string GetErrorMessage(PortCellState cell, ValidationContext ctx);

	protected virtual void OnSuccess(PortCellState cell, ValidationContext ctx){ }
	protected virtual void OnError(PortCellState cell, ValidationContext ctx){ }
	protected virtual void Clear(PortCellState cell, ValidationContext ctx) { }

	public static PortHandler? Create(List<PortHandler> handlers){
		if(!handlers.Any()) return null;
		PortHandler current = handlers[0];
		for(int i = 1; i < handlers.Count; i++){
			current.SetNext(handlers[i]);
			current = handlers[i];
		}
		return handlers[0];
	}
	
	private static PortHandler CreateChain(){
		return Create([
			new IdNotEmpty(),
			new IdNotContainSpaces(),
			new IdMatchRegex(),
			new IdNotDuplicated(),
			new ExpNotEmpty(),
			new ExpSyntaxAnalyzer(),
			new ExpHasValidIds(),
			new ExpSemanticAnalyzer()
		])!;
	}

	public static ValidationContext Validate(List<InputCellState> inputs, List<OutputCellState> outputs){
		Debug.Log("Validating ports...");
		PortHandler validator = CreateChain();
		var context = new ValidationContext(inputs, outputs);

		foreach(InputCellState i in inputs)
			validator.Handle(i, context);
		
		foreach(OutputCellState o in outputs)
			validator.Handle(o, context);
		
		var errors = context.GetErrors();
		Debug.Log("Erros found: " + errors.Count);

		foreach(string err in errors){
			Debug.Log("\t " + err);
		}
		
		if(errors.Count > 0)
			throw new Exception(string.Join("\n", errors));

		return context;
	}
}
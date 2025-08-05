using CircuitNotIncluded.UI.Cells;

namespace CircuitNotIncluded.UI.Validators;

public abstract class PortHandler(PortHandler? next) {
	private PortHandler? next = next;

	public void SetNext(PortHandler next){
		this.next = next;
	}

	public void Handle(PortCellType cell, ValidationContext ctx){
		Clear(cell, ctx);
		if(CanHandle(cell, ctx) && ErrorOccurred(cell, ctx)){
			OnError(cell, ctx);
			RegisterError(cell, ctx);
			return;
		}
		OnSuccess(cell, ctx);
		Next(cell, ctx);
	}

	private bool MustChainNext(PortCellType cell, ValidationContext ctx){
		if(CanHandle(cell, ctx)) return false;
		if(ErrorOccurred(cell, ctx)){
			OnError(cell, ctx);
			return false;
		}
		OnSuccess(cell, ctx);
		return true;
	}

	protected virtual bool CanHandle(PortCellType cell, ValidationContext ctx) => true;
	protected abstract bool ErrorOccurred(PortCellType cell, ValidationContext ctx);

	private void Next(PortCellType cell, ValidationContext ctx){
		next?.Handle(cell, ctx);
	}
	
	private void RegisterError(PortCellType cell, ValidationContext ctx){
		ctx.AddError(cell, GetErrorMessage(cell, ctx));
	}
	
	protected abstract string GetErrorMessage(PortCellType cell, ValidationContext ctx);

	protected virtual void OnSuccess(PortCellType cell, ValidationContext ctx){ }
	protected virtual void OnError(PortCellType cell, ValidationContext ctx){ }
	protected virtual void Clear(PortCellType cell, ValidationContext ctx) { }

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

	public static ValidationContext Validate(List<InputCellType> inputs, List<OutputCellType> outputs){
		PortHandler validator = CreateChain();
		var context = new ValidationContext(inputs, outputs);

		foreach(InputCellType i in inputs)
			validator.Handle(i, context);
		
		foreach(OutputCellType o in outputs)
			validator.Handle(o, context);
		
		var errors = context.GetErrors();
		if(errors.Count > 0)
			throw new Exception(string.Join("\n", errors));

		return context;
	}
}
using CircuitNotIncluded.UI.Cells;

namespace CircuitNotIncluded.UI.Validators;

public interface IPortValidator<in TPort> where TPort : PortCellState {
    bool Validate(TPort port, ValidationContext ctx);
}

public abstract class BasePortValidator<TPort> : IPortValidator<TPort> where TPort : PortCellState {

    protected abstract bool DispatchErrorWhen(TPort port, ValidationContext ctx);

    protected abstract string GetErrorMessage(TPort port, ValidationContext ctx);
    
    protected virtual void OnSuccess(TPort port, ValidationContext ctx) { }
    
    protected virtual void BeforeEach(TPort port, ValidationContext ctx) { }
    
    public bool Validate(TPort port, ValidationContext ctx){
        BeforeEach(port, ctx);
        bool errorOccurred = DispatchErrorWhen(port, ctx);
        if (errorOccurred) {
            ctx.AddError(port, GetErrorMessage(port, ctx));
            return false;
        }
        OnSuccess(port, ctx);
        return true;
    }
}

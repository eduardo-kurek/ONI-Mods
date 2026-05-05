using CircuitNotIncluded.UI.Cells;

namespace CircuitNotIncluded.UI.Validators;
 
public class ValidatorChain<TPort> where TPort : PortCellState {
    private readonly List<IPortValidator<TPort>> validators = [];
    
    public void Add(IPortValidator<TPort> validator){
        validators.Add(validator);
    }

    public void ValidateAll(List<TPort> ports, ValidationContext ctx) {
        foreach(var port in ports){
            foreach(var validator in validators){
                if (!validator.Validate(port, ctx))
                    break;
            }
        }
    }
}

public static class Validator {
    public static ValidationContext Validate(List<InputCellState> inputs, List<OutputCellState> outputs){
        var context = new ValidationContext(inputs, outputs);
		
        ValidatorChain<InputCellState> inputValidators = new ();
        ValidatorChain<OutputCellState> outputValidators = new ();
		
        inputValidators.Add(new IdNotEmptyValidator());
        inputValidators.Add(new IdNotContainSpacesValidator());
        inputValidators.Add(new IdMatchRegexValidator());
        inputValidators.Add(new IdNotDuplicatedValidator());
		
        outputValidators.Add(new IdNotEmptyValidator());
        outputValidators.Add(new IdNotContainSpacesValidator());
        outputValidators.Add(new IdMatchRegexValidator());
        outputValidators.Add(new IdNotDuplicatedValidator());
        outputValidators.Add(new ExpNotEmptyValidator());
        outputValidators.Add(new ExpSyntaxValidator());
        outputValidators.Add(new ExpHasValidIdsValidator());
        outputValidators.Add(new ExpSemanticValidator());

        inputValidators.ValidateAll(inputs, context);
        outputValidators.ValidateAll(outputs, context);
		
        var errors = context.GetErrors();
		
        if(errors.Count > 0)
            throw new Exception(string.Join("\n", errors));

        return context;
    }
}
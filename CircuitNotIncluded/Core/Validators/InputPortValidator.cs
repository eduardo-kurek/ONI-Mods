using CircuitNotIncluded.Core.Model;
using FluentValidation;

namespace CircuitNotIncluded.Core.Validators;

public class InputPortValidator : AbstractValidator<InputPortModel> {
	public InputPortValidator(Dictionary<string, InputBitModel> declaredInputs){
		CascadeMode = CascadeMode.Stop;

		RuleFor(p => p.Bit1)
			.SetValidator(new InputBitValidator(declaredInputs));
	}	
}
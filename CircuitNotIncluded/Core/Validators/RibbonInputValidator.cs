using CircuitNotIncluded.Core.Model;
using FluentValidation;

namespace CircuitNotIncluded.Core.Validators;

public class RibbonInputValidator : AbstractValidator<RibbonInputModel> {
	public RibbonInputValidator(Dictionary<string, InputBitModel> declaredInputs){
		RuleFor(p => p.Bit1)
			.SetValidator(new InputBitValidator(declaredInputs, true));
		
		RuleFor(p => p.Bit2)
			.SetValidator(new InputBitValidator(declaredInputs, true));
		
		RuleFor(p => p.Bit3)
			.SetValidator(new InputBitValidator(declaredInputs, true));
		
		RuleFor(p => p.Bit4)
			.SetValidator(new InputBitValidator(declaredInputs, true));
	}	
}
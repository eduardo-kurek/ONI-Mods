using CircuitNotIncluded.Core.Model;
using FluentValidation;

namespace CircuitNotIncluded.Core.Validators;

public class RibbonOutputValidator : AbstractValidator<RibbonOutputModel> {
	public RibbonOutputValidator(ValidationData data){
		RuleFor(p => p.Bit1)
			.SetValidator(new OutputBitValidator(data, true));
		
		RuleFor(p => p.Bit2)
			.SetValidator(new OutputBitValidator(data, true));
		
		RuleFor(p => p.Bit3)
			.SetValidator(new OutputBitValidator(data, true));
		
		RuleFor(p => p.Bit4)
			.SetValidator(new OutputBitValidator(data, true));}	
}
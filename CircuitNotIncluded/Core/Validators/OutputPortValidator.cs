using CircuitNotIncluded.Core.Model;
using FluentValidation;

namespace CircuitNotIncluded.Core.Validators;

public class OutputPortValidator : AbstractValidator<OutputPortModel> {
	public OutputPortValidator(ValidationData data){
		RuleFor(p => p.Bit1)
			.SetValidator(new OutputBitValidator(data));	
	}
}
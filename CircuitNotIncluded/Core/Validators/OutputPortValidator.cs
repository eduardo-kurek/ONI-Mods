using CircuitNotIncluded.Core.Model;
using FluentValidation;

namespace CircuitNotIncluded.Core.Validators;

public class OutputPortValidator : AbstractValidator<OutputPortModel> {
	public OutputPortValidator(ValidationData data){
		CascadeMode = CascadeMode.Stop;

		RuleFor(p => p.Bit1)
			.SetValidator(new OutputBitValidator(data));	
	}
}
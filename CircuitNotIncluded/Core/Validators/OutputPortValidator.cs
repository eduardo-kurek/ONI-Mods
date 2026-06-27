using CircuitNotIncluded.Core.Model;
using FluentValidation;

namespace CircuitNotIncluded.Core.Validators;

public class OutputPortValidator : AbstractValidator<OutputPortModel> {
	public OutputPortValidator(
		Dictionary<string, InputBitModel> declaredInputs,
		Dictionary<string, OutputBitModel> declaredOutputs)
	{
		CascadeMode = CascadeMode.Stop;

		RuleFor(p => p.Bit1)
			.SetValidator(new OutputBitValidator(declaredInputs, declaredOutputs));	
	}
}
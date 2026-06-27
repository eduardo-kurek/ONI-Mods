using CircuitNotIncluded.Core.Model;
using FluentValidation;
using FluentValidation.Results;

namespace CircuitNotIncluded.Core.Validators;

public class CircuitValidator : AbstractValidator<CircuitModel> {
	private CircuitValidator(){
		var declaredInputs = new Dictionary<string, InputBitModel>(StringComparer.Ordinal);
		var declaredOutputs = new Dictionary<string, OutputBitModel>(StringComparer.Ordinal);
		
		RuleFor(circuit => circuit.Name)
			.Cascade(CascadeMode.Stop)
			.NotEmpty().WithMessage("Circuit name cannot be empty")
			.MinimumLength(3).WithMessage("Circuit name must be at least 3 characters long");

		RuleForEach(c => c.InputPorts)
			.SetValidator(new InputPortValidator(declaredInputs))
			.DependentRules(() => {
				RuleForEach(c => c.OutputPorts)
					.SetValidator(new OutputPortValidator(declaredInputs, declaredOutputs));
			});
	}

	public static void DoValidate(CircuitModel circuit){
		var validator = new CircuitValidator();
		ValidationResult result = validator.Validate(circuit);
		if(result.IsValid) return;
		
		var outputErrors = result.Errors.Select(e => e.ErrorMessage);
		throw new Exception(string.Join("\n", outputErrors));
	}
}
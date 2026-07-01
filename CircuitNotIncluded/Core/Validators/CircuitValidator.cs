using CircuitNotIncluded.Core.Model;
using FluentValidation;
using FluentValidation.Results;

namespace CircuitNotIncluded.Core.Validators;

public class CircuitValidator : AbstractValidator<CircuitModel> {
	private CircuitValidator(){
		ValidationData data = new();
		
		RuleFor(circuit => circuit.Name)
			.Cascade(CascadeMode.Stop)
			.NotEmpty().WithMessage("Circuit name cannot be empty")
			.MinimumLength(3).WithMessage("Circuit name must be at least 3 characters long");

		RuleFor(circuit => circuit.PortModels)
			.Custom((models, context) => {
				if (models == null || models.Length == 0) return;

				var modelsByPriority = models
					.GroupBy(m => m.ValidationPriority)
					.OrderBy(g => g.Key);

				foreach(var group in modelsByPriority) {
					var failures = new List<ValidationFailure>();

					foreach(var item in group){
						var result = item.Validate(data);
						if(!result.IsValid)
							failures.AddRange(result.Errors);
					}

					if(failures.Count > 0){
						foreach (var failure in failures)
							context.AddFailure(failure);
						return;
					}
				}
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
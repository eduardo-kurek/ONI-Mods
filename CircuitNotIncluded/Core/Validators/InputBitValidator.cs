using System.Text;
using CircuitNotIncluded.Core.Model;
using FluentValidation;
using static CircuitNotIncluded.Grammar.ExpressionUtils;

namespace CircuitNotIncluded.Core.Validators;

public class InputBitValidator : AbstractValidator<InputBitModel> {
	private const string RegexPattern = @"^[a-zA-Z_][a-zA-Z0-9_]*$";
	
	public InputBitValidator(Dictionary<string, InputBitModel> declaredInputs, bool withBitNumber = false){
		CascadeMode = CascadeMode.Stop;

		RuleFor(i => i.Id)
			.Cascade(CascadeMode.Stop)
			.NotEmpty().WithMessage(i => $"{Prefix(i)} ID cannot be empty.")
			.Must(id => !id.Contains(" ")).WithMessage(i => $"{Prefix(i)} ID '{i.Id}' cannot contain spaces.")
			.Must(id => !char.IsDigit(id[0])).WithMessage(i => $"{Prefix(i)} ID '{i.Id}' cannot start with a number.")
			.Matches(RegexPattern).WithMessage(i => $"{Prefix(i)} ID '{i.Id}' contains invalid characters.")
			.Must(id => !ReservedWords.Contains(id)).WithMessage(i => $"{Prefix(i)} ID '{i.Id}' is a reserved word.");

		RuleFor(i => i)
			.Custom((i, ctx) => {
				if (declaredInputs.TryGetValue(i.Id, out var declared))
					ctx.AddFailure($"{Prefix(i)} ID '{i.Id}' duplicated at cell {declared.Port.Index}.{i.BitNumber}");
				else
					declaredInputs[i.Id] = i;
			});
		
		return;
		string Prefix(InputBitModel i) => MsgPrefix(i, withBitNumber);
	}

	private static string MsgPrefix(InputBitModel i, bool withBitNumber){
		StringBuilder sb = new();
		sb.Append($"({i.Port.Index}");
		if (withBitNumber) sb.Append($".{i.BitNumber}");
		sb.Append(")");
		return sb.ToString();
	}
}
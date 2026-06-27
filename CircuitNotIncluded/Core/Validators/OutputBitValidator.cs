using System.Text;
using CircuitNotIncluded.Core.Model;
using CircuitNotIncluded.Grammar;
using FluentValidation;
using static CircuitNotIncluded.Grammar.ExpressionUtils;

namespace CircuitNotIncluded.Core.Validators;

public class OutputBitValidator : AbstractValidator<OutputBitModel> {
	private const string RegexPattern = @"^[a-zA-Z_][a-zA-Z0-9_]*$";
	
	public OutputBitValidator(
		Dictionary<string, InputBitModel> declaredInputs,
		Dictionary<string, OutputBitModel> declaredOutputs,
		bool withBitNumber = false)
	{
		CascadeMode = CascadeMode.Stop;
		
		RuleFor(o => o.Label)
			.Cascade(CascadeMode.Stop)
			.NotEmpty().WithMessage(o => $"{Prefix(o)} Label cannot be empty.")
			.Must(label => !label.Contains(" ")).WithMessage(o => $"{Prefix(o)} Label '{o.Label}' cannot contain spaces.")
			.Must(label => !char.IsDigit(label[0])).WithMessage(o => $"{Prefix(o)} Label '{o.Label}' cannot start with a number.")
			.Matches(RegexPattern).WithMessage(o => $"{Prefix(o)} Label '{o.Label}' contains invalid characters.")
			.Must(label => !ReservedWords.Contains(label)).WithMessage(o => $"{Prefix(o)} Label '{o.Label}' is a reserved word.");

		RuleFor(o => o)
			.Cascade(CascadeMode.Stop)
			.Custom((o, ctx) => {
				if (declaredInputs.TryGetValue(o.Label, out var declared)){
					ctx.AddFailure(
						$"{Prefix(o)} Label '{o.Label}' conflicts with input ID declared at cell {declared.Port.Index}.{declared.BitNumber}");
				}
			})
			.Custom((o, ctx) => {
				if (declaredOutputs.TryGetValue(o.Label, out var declared))
					ctx.AddFailure($"{Prefix(o)} Label '{o.Label}' duplicated at cell {declared.Port.Index}.{o.BitNumber}");
				else
					declaredOutputs[o.Label] = o;
			});
		
		RuleFor(o => o)
			.Cascade(CascadeMode.Stop)
			.Custom((o, ctx) => {
				try{ Compiler.Parse(o.Expression); }
				catch(Exception e){ ctx.AddFailure($"{Prefix(o)} {e.Message}"); }
			})
			.Custom((o, ctx) => {
				var ids = new HashSet<string>(declaredInputs.Keys);
				try{ Compiler.SemanticAnalyze(o.Expression, ids); }
				catch(Exception e){ ctx.AddFailure($"{Prefix(o)} {e.Message}"); }
			});
			
		return;
		string Prefix(OutputBitModel o) => MsgPrefix(o, withBitNumber);
	}
	
	private static string MsgPrefix(OutputBitModel o, bool withBitNumber) { 
		StringBuilder sb = new();
		sb.Append($"({o.Port.Index}");
		if (withBitNumber) sb.Append($".{o.BitNumber}");
		sb.Append(")");
		return sb.ToString();
	}
}
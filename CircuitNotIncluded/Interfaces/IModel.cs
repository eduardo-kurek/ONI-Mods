using CircuitNotIncluded.Core.Validators;
using CircuitNotIncluded.Grammar.Visitors.Expression;
using FluentValidation.Results;

namespace CircuitNotIncluded.Interfaces;

public enum ValidationPriority {
	First,
	Second
}

public interface IModel {
	ValidationPriority ValidationPriority { get; }
	IRuntime CreateRuntime(SymbolTable symbolTable, Dictionary<string, ExpressionState> statesByLabel);
	ValidationResult Validate(ValidationData data);
}
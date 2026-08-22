using CircuitNotIncluded.Core.DTO;
using CircuitNotIncluded.Core.Validators;
using CircuitNotIncluded.Grammar.Visitors.Expression;
using CircuitNotIncluded.Interfaces;
using FluentValidation.Results;

namespace CircuitNotIncluded.Core.Model;

public delegate int OffsetResolver(CellOffset offset);

public abstract class PortModel(PortDTO port, CircuitModel circuit, OffsetResolver resolver) : IModel {
	
	public abstract ValidationPriority ValidationPriority { get; }
	public CellOffset Offset { get; } = port.Offset;
	public int Index { get; } = resolver.Invoke(port.Offset);
	public CircuitModel Circuit { get; } = circuit;
	
	public abstract IRuntime CreateRuntime(
		SymbolTable symbolTable,
		Dictionary<string, ExpressionState> statesByLabel
	);
	
	public abstract ValidationResult Validate(ValidationData data);
	
}
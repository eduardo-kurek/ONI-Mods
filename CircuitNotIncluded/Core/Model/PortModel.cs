using CircuitNotIncluded.Core.DTO;
using CircuitNotIncluded.Grammar;
using CircuitNotIncluded.Interfaces;

namespace CircuitNotIncluded.Core.Model;

public delegate int OffsetResolver(CellOffset offset);

public abstract class PortModel(PortDTO port, CircuitModel circuit, OffsetResolver resolver) : IModel {
	public CellOffset Offset { get; } = port.Offset;
	public int Index { get; } = resolver.Invoke(port.Offset);
	public CircuitModel Circuit { get; } = circuit;
	public abstract IRuntime CreateRuntime(SymbolTable symbolTable);
}
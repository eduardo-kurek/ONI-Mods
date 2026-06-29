using CircuitNotIncluded.Grammar;

namespace CircuitNotIncluded.Interfaces;

public interface IModel {
	IRuntime CreateRuntime(SymbolTable symbolTable);
}
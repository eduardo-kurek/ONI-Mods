using CircuitNotIncluded.Core.DTO;
using CircuitNotIncluded.Grammar;
using CircuitNotIncluded.Interfaces;

namespace CircuitNotIncluded.Core.Model;

public class CircuitModel {
	public string Name { get; }
	public IModel[] PortModels { get; }

	public CircuitModel(CircuitDTO circuit, OffsetResolver resolver) {
		Name = circuit.Name;

		PortModels = circuit.Ports
			.OrderBy(p => p.Offset.x).ThenBy(p => p.Offset.y)
			.Select(p => p.CreateModel(this, resolver))
			.ToArray();
	}

	public IRuntime[] CreateRuntimes(){
		SymbolTable symbolTable = new();
		return [
			..PortModels.Select(p => p.CreateRuntime(symbolTable))
		];
	}
}
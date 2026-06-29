using CircuitNotIncluded.Core.DTO;
using CircuitNotIncluded.Grammar;
using CircuitNotIncluded.Interfaces;

namespace CircuitNotIncluded.Core.Model;

public class CircuitModel {
	public string Name { get; }
	public InputPortModel[] InputPorts { get; }
	public OutputPortModel[] OutputPorts { get; }

	public CircuitModel(CircuitDTO circuit, OffsetResolver resolver) {
		Name = circuit.Name;
      
		InputPorts = circuit.InputPorts
			.Select(p => new InputPortModel(p, this, resolver))
			.ToArray();

		OutputPorts = circuit.OutputPorts
			.Select(p => new OutputPortModel(p, this, resolver))
			.ToArray();
	}

	public IRuntime[] CreateRuntimes(){
		SymbolTable symbolTable = new();
		return [
			..InputPorts.Select(p => p.CreateRuntime(symbolTable)),
			..OutputPorts.Select(p => p.CreateRuntime(symbolTable))
		];
	}
}
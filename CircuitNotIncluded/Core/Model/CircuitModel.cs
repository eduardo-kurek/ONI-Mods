using CircuitNotIncluded.Core.DTO;
using CircuitNotIncluded.Core.Interfaces;

namespace CircuitNotIncluded.Core.Model;

/**
 * Class that wrap DTO and link references between parents and childs
 * Acts like a proxy
 */
public class CircuitModel : ICircuit {
	public string Name { get; }
	public InputPortModel[] InputPorts { get; }
	public OutputPortModel[] OutputPorts { get; }

	IInputPort[] ICircuit.InputPorts => InputPorts;
	IOutputPort[] ICircuit.OutputPorts => OutputPorts;
	
	public IReadOnlyList<InputBitModel> InputBits
		=> InputPorts
			.SelectMany(port => new[] { port.Bit1 })
			.ToList()
			.AsReadOnly();
	
	public IReadOnlyList<OutputBitModel> OutputBits 
		=> OutputPorts
			.SelectMany(port => new[] { port.Bit1 })
			.ToList()
			.AsReadOnly();

	public CircuitModel(ICircuit circuit, OffsetResolver resolver) {
		Name = circuit.Name;
      
		InputPorts = circuit.InputPorts
			.Select(p => new InputPortModel(p, this, resolver))
			.ToArray();

		OutputPorts = circuit.OutputPorts
			.Select(p => new OutputPortModel(p, this, resolver))
			.ToArray();
	}
}
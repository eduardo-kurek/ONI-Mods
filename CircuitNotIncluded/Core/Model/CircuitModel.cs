using CircuitNotIncluded.Core.DTO;

namespace CircuitNotIncluded.Core.Model;

public class CircuitModel {
	public string Name { get; }
	public InputPortModel[] InputPorts { get; }
	public OutputPortModel[] OutputPorts { get; }

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

	public CircuitModel(CircuitDTO circuit, OffsetResolver resolver) {
		Name = circuit.Name;
      
		InputPorts = circuit.InputPorts
			.Select(p => new InputPortModel(p, this, resolver))
			.ToArray();

		OutputPorts = circuit.OutputPorts
			.Select(p => new OutputPortModel(p, this, resolver))
			.ToArray();
	}
}
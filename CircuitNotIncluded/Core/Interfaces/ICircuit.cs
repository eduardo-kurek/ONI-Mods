namespace CircuitNotIncluded.Core.Interfaces;

public interface ICircuit {
	string Name { get; }
	IInputPort[] InputPorts { get; }
	IOutputPort[] OutputPorts { get; }
}
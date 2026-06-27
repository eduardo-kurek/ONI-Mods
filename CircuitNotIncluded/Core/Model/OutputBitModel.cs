using CircuitNotIncluded.Core.Interfaces;

namespace CircuitNotIncluded.Core.Model;

public class OutputBitModel(IOutputBit bit, PortModel port, int bitNumber) : IOutputBit {
	public string Label { get; } = bit.Label;
	public string Description { get; } = bit.Description;
	public string Expression { get; } = bit.Expression;
	public int BitNumber { get; } = bitNumber;
	public PortModel Port { get; } = port;
}
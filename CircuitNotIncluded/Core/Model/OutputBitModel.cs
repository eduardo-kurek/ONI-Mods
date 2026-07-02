using CircuitNotIncluded.Core.DTO;

namespace CircuitNotIncluded.Core.Model;

public class OutputBitModel(OutputBitDTO bit, PortModel port, int bitNumber) {
	public string Label { get; } = bit.Label;
	public string Description { get; } = bit.Description;
	public string Expression { get; } = bit.Expression;
	public int BitNumber { get; } = bitNumber;
	public PortModel Port { get; } = port;
}
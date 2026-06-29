using CircuitNotIncluded.Core.DTO;

namespace CircuitNotIncluded.Core.Model;

public class InputBitModel(InputBitDTO bit, PortModel port, int bitNumber) {
	public string Id { get; } = bit.Id;
	public string Description { get; } = bit.Description;
	public int BitNumber { get; } = bitNumber;
	public PortModel Port { get; } = port;
}
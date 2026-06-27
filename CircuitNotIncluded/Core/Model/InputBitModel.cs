using CircuitNotIncluded.Core.Interfaces;

namespace CircuitNotIncluded.Core.Model;

public class InputBitModel(IInputBit bit, PortModel port, int bitNumber) : IInputBit {
	public string Id { get; } = bit.Id;
	public string Description { get; } = bit.Description;
	public int BitNumber { get; } = bitNumber;
	public PortModel Port { get; } = port;
}
using CircuitNotIncluded.Core.Interfaces;

namespace CircuitNotIncluded.Core.Model;

public delegate int OffsetResolver(CellOffset offset);

public class PortModel(IPort port, CircuitModel circuit, OffsetResolver resolver) : IPort {
	public CellOffset Offset { get; } = port.Offset;
	public int Index { get; } = resolver.Invoke(port.Offset);
	public CircuitModel Circuit { get; } = circuit;
}
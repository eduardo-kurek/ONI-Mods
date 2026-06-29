using CircuitNotIncluded.Core.DTO;

namespace CircuitNotIncluded.Core.Model;

public delegate int OffsetResolver(CellOffset offset);

public class PortModel(PortDTO port, CircuitModel circuit, OffsetResolver resolver) {
	public CellOffset Offset { get; } = port.Offset;
	public int Index { get; } = resolver.Invoke(port.Offset);
	public CircuitModel Circuit { get; } = circuit;
}
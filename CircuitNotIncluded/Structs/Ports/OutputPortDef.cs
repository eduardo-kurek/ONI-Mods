using KSerialization;

namespace CircuitNotIncluded.Structs.Ports;

[SerializationConfig(MemberSerialization.OptIn)]
public struct OutputPortDef {
	[Serialize] public CellOffset Offset;
	[Serialize] public OutputBit Output1;
}
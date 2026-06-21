using KSerialization;

namespace CircuitNotIncluded.Structs.Ports;

[SerializationConfig(MemberSerialization.OptIn)]
public struct InputPortDef {
	[Serialize] public CellOffset Offset;
	[Serialize] public InputBit Input1;
}
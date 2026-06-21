using KSerialization;

namespace CircuitNotIncluded.Structs.Ports;

[SerializationConfig(MemberSerialization.OptIn)]
public struct CircuitDTO {
	[Serialize] public List<InputPortDef> InputPorts;
	[Serialize] public List<OutputPortDef> OutputPorts;
}
using KSerialization;

namespace CircuitNotIncluded.Structs.Ports;

[SerializationConfig(MemberSerialization.OptIn)]
public struct InputBit {
	[Serialize] public string Id;
	[Serialize] public string Description;
}
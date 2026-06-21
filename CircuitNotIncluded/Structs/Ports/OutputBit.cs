using KSerialization;

namespace CircuitNotIncluded.Structs.Ports;

[SerializationConfig(MemberSerialization.OptIn)]
public struct OutputBit {
	[Serialize] public string Label;
	[Serialize] public string Description;
	[Serialize] public string Expression;
}
namespace CircuitNotIncluded.Structs;

public interface IInputSource {
	int GetInputPortValue(HashedString portId);
}
using CircuitNotIncluded.Core.Model;

namespace CircuitNotIncluded.Core.Validators;

public class ValidationData {
	public readonly Dictionary<string, InputBitModel> declaredInputs = new();
	public readonly Dictionary<string, OutputBitModel> declaredOutputs = new();
}
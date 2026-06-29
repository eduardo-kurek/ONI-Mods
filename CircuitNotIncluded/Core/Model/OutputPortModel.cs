using CircuitNotIncluded.Core.DTO;

namespace CircuitNotIncluded.Core.Model;

public class OutputPortModel : PortModel {
	public OutputBitModel Bit1 { get; }

	public OutputPortModel(OutputPortDTO outputPort, CircuitModel circuit, OffsetResolver resolver) 
		: base(outputPort, circuit, resolver) {
		Bit1 = new OutputBitModel(outputPort.Bit1, this, 1);
	}
}
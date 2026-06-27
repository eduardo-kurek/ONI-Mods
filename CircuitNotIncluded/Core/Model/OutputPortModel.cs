using CircuitNotIncluded.Core.Interfaces;

namespace CircuitNotIncluded.Core.Model;

public class OutputPortModel : PortModel, IOutputPort {
	public OutputBitModel Bit1 { get; }

	IOutputBit IOutputPort.Bit1 => Bit1;
  
	public OutputPortModel(IOutputPort outputPort, CircuitModel circuit, OffsetResolver resolver) 
		: base(outputPort, circuit, resolver) {
		Bit1 = new OutputBitModel(outputPort.Bit1, this, 1);
	}
}
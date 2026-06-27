using CircuitNotIncluded.Core.Interfaces;

namespace CircuitNotIncluded.Core.Model;

public class InputPortModel : PortModel, IInputPort {
	public InputBitModel Bit1 { get; }
	
	IInputBit IInputPort.Bit1 => Bit1;

	public InputPortModel(IInputPort inputPort, CircuitModel circuit, OffsetResolver resolver) 
		: base(inputPort, circuit, resolver) {
		Bit1 = new InputBitModel(inputPort.Bit1, this, 1);
	}
}
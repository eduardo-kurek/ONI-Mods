using CircuitNotIncluded.Core.DTO;

namespace CircuitNotIncluded.Core.Model;

public class InputPortModel : PortModel {
	public InputBitModel Bit1 { get; }

	public InputPortModel(InputPortDTO inputPort, CircuitModel circuit, OffsetResolver resolver) 
		: base(inputPort, circuit, resolver) {
		Bit1 = new InputBitModel(inputPort.Bit1, this, 1);
	}
}
using CircuitNotIncluded.Core.DTO;
using CircuitNotIncluded.Core.Runtime;
using CircuitNotIncluded.Grammar;
using CircuitNotIncluded.Interfaces;

namespace CircuitNotIncluded.Core.Model;

public class InputPortModel : PortModel {
	public InputBitModel Bit1 { get; }

	public InputPortModel(InputPortDTO inputPort, CircuitModel circuit, OffsetResolver resolver) 
		: base(inputPort, circuit, resolver) {
		Bit1 = new InputBitModel(inputPort.Bit1, this, 1);
	}

	public override IRuntime CreateRuntime(SymbolTable symbolTable){
		return new InputRuntime(symbolTable, Bit1.Id, Index);
	}
}
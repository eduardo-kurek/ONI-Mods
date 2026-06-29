using CircuitNotIncluded.Core.DTO;
using CircuitNotIncluded.Core.Runtime;
using CircuitNotIncluded.Grammar;
using CircuitNotIncluded.Interfaces;

namespace CircuitNotIncluded.Core.Model;

public class OutputPortModel : PortModel {
	public OutputBitModel Bit1 { get; }

	public OutputPortModel(OutputPortDTO outputPort, CircuitModel circuit, OffsetResolver resolver) 
		: base(outputPort, circuit, resolver) {
		Bit1 = new OutputBitModel(outputPort.Bit1, this, 1);
	}

	public override IRuntime CreateRuntime(SymbolTable symbolTable){
		return new OutputRuntime(symbolTable, Bit1.Expression, Index);
	}
}
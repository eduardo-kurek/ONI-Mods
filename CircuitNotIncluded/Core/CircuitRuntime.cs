using CircuitNotIncluded.Grammar;
using CircuitNotIncluded.Structs;
using CircuitNotIncluded.Structs.Ports;

namespace CircuitNotIncluded.Core;

public class CircuitRuntime {
	private List<CircuitInput> Inputs = [];
	private List<CircuitOutput> Outputs = [];
	private SymbolTable symbolTable = new();
	
	public CircuitRuntime(Circuit circuit, CircuitDTO circuitDto){
		foreach(InputPort i in circuitDto.InputPorts){
			string id = i.Bit1.Id;
			int cell = circuit.GetActualCell(i.Offset);
			Inputs.Add(new CircuitInput(symbolTable, id, cell));
		}

		foreach(OutputPort i in circuitDto.OutputPorts){
			var evaluateFunc = Compiler.Compile(i.Bit1.Expression);
			int cell = circuit.GetActualCell(i.Offset);
			Outputs.Add(new CircuitOutput(symbolTable, evaluateFunc, cell));
		}
	}

	public void Connect(){
		foreach(var i in Inputs)
			i.Connect();
		foreach(var o in Outputs)
			o.Connect();
	}

	public void Disconnect(){
		foreach(var i in Inputs)
			i.Disconnect();
		foreach(var o in Outputs)
			o.Disconnect();	
	}
}
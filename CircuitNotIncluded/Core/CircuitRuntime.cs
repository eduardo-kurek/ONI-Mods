using CircuitNotIncluded.Grammar;
using CircuitNotIncluded.Structs;
using CircuitNotIncluded.Structs.Ports;
using UnityEngine;

namespace CircuitNotIncluded.Core;

public class CircuitRuntime {
	private readonly List<InputRuntime> inputs = [];
	private readonly List<OutputRuntime> outputs = [];
	private readonly SymbolTable symbolTable = new();
	
	public CircuitRuntime(Circuit circuit){
		foreach(InputPort i in circuit.dto.InputPorts){
			string id = i.Bit1.Id;
			int cell = circuit.GetActualCell(i.Offset);
			inputs.Add(new InputRuntime(symbolTable, id, cell));
		}

		foreach(OutputPort i in circuit.dto.OutputPorts){
			var evaluateFunc = Compiler.Compile(i.Bit1.Expression);
			int cell = circuit.GetActualCell(i.Offset);
			outputs.Add(new OutputRuntime(symbolTable, evaluateFunc, cell));
		}
	}

	public void Connect(){
		foreach(var i in inputs)
			i.Connect();
		foreach(var o in outputs)
			o.Connect();
	}

	public void Disconnect(){
		foreach(var i in inputs)
			i.Disconnect();
		foreach(var o in outputs)
			o.Disconnect();	
	}
}
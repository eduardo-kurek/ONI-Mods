using CircuitNotIncluded.Core.DTO;
using CircuitNotIncluded.Core.Model;
using CircuitNotIncluded.Grammar;

namespace CircuitNotIncluded.Core.Runtime;

public class CircuitRuntime {
	private readonly List<InputRuntime> inputs = [];
	private readonly List<OutputRuntime> outputs = [];
	private readonly SymbolTable symbolTable = new();
	
	public CircuitRuntime(CircuitModel circuit, Func<CellOffset, int> offsetToCell){
		foreach(var i in circuit.InputBits){
			string id = i.Id;
			int cell = offsetToCell.Invoke(i.Port.Offset);
			inputs.Add(new InputRuntime(symbolTable, id, cell));
		}

		foreach(var o in circuit.OutputBits){
			int cell = offsetToCell.Invoke(o.Port.Offset);
			outputs.Add(new OutputRuntime(symbolTable, o.Expression, cell));
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
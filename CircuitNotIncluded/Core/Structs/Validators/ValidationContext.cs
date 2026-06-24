using CircuitNotIncluded.Grammar;
using CircuitNotIncluded.UI.Cells;
using static CircuitNotIncluded.Grammar.ExpressionParser;

namespace CircuitNotIncluded.Core.Structs.Validators;

public class ValidationContext {
	private readonly List<string> Errors = [];
	
	private readonly HashSet<string> InputIds = [];
	private readonly HashSet<string> OutputIds = [];
	
	private readonly Dictionary<string, PortCellState> DeclaredIds = [];
	private readonly Dictionary<OutputCellState, ProgramContext> ParsedOutputs = [];

	public ValidationContext(List<InputCellState> inputs, List<OutputCellState> outputs){
		foreach(InputCellState i in inputs)
			InputIds.Add(i.GetId());
		foreach(OutputCellState o in outputs)
			OutputIds.Add(o.GetId());
	}

	public bool HasOutputId(string id) {
		return OutputIds.Contains(id);
	}

	public bool IsPortDeclared(PortCellState port) => DeclaredIds.ContainsKey(port.GetId());
	public void DeclarePort(PortCellState port) => DeclaredIds[port.GetId()] = port;
	public PortCellState GetDeclaredPort(string id) => DeclaredIds[id];

	public HashSet<string> GetInputIds(){
		return InputIds;
	}
	
	public void AddError(PortCellState cell, string msg){
		Errors.Add($"({cell.GetDisplayIndex()}). {msg}");
	}
	
	public ProgramContext Parse(OutputCellState cell){
		if(ParsedOutputs.TryGetValue(cell, out ProgramContext output)) return output;
		string expression = cell.GetExpression();
		ProgramContext tree = Compiler.Parse(expression);
		ParsedOutputs.Add(cell, tree);
		return tree;
	}

	public List<string> GetErrors(){
		return Errors;
	}
}
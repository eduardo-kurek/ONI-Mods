using CircuitNotIncluded.Grammar;
using CircuitNotIncluded.Structs;
using CircuitNotIncluded.UI.Cells;
using static CircuitNotIncluded.Grammar.ExpressionParser;

namespace CircuitNotIncluded.UI.Validators;

public class ValidationContext {
	private readonly List<string> Errors = [];
	
	private readonly HashSet<string> InputIds = [];
	private readonly HashSet<string> OutputIds = [];
	
	private readonly List<OutputCellState> Outputs;
	private readonly List<InputCellState> Inputs;
	
	public readonly Dictionary<string, CellOffset> DeclaredIds = [];
	private readonly Dictionary<OutputCellState, ProgramContext> ParsedOutputs = [];

	public ValidationContext(List<InputCellState> inputs, List<OutputCellState> outputs){
		Inputs = inputs;
		Outputs = outputs;
		foreach(InputCellState i in inputs)
			InputIds.Add(i.GetId());
		foreach(OutputCellState o in outputs)
			OutputIds.Add(o.GetId());
	}

	public bool HasOutputId(string id) {
		return OutputIds.Contains(id);
	}

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

	public List<InputPort> ResultInput(){
		List<InputPort> result = [];
		result.AddRange(Inputs.Select(i => i.ToPort()));
		return result;
	}

	public List<OutputPort> ResultOutput(){
		List<OutputPort> result = [];
		result.AddRange(Outputs.Select(i => i.ToPort(ParsedOutputs[i])));
		return result;
	}
}
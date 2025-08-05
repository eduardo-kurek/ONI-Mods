using CircuitNotIncluded.Structs;
using CircuitNotIncluded.UI.Cells;
using static CircuitNotIncluded.Grammar.ExpressionParser;

namespace CircuitNotIncluded.UI.Validators;

public class ValidationContext {
	private readonly List<string> Errors = [];
	
	private readonly HashSet<string> InputIds = [];
	private readonly HashSet<string> OutputIds = [];
	
	private readonly List<OutputCellType> Outputs;
	private readonly List<InputCellType> Inputs;
	
	public readonly Dictionary<string, CellOffset> DeclaredIds = [];
	private readonly Dictionary<OutputCellType, ProgramContext> ParsedOutputs = [];

	public ValidationContext(List<InputCellType> inputs, List<OutputCellType> outputs){
		Inputs = inputs;
		Outputs = outputs;
		foreach(InputCellType i in inputs)
			InputIds.Add(i.GetId());
		foreach(OutputCellType o in outputs)
			OutputIds.Add(o.GetId());
	}

	public bool HasOutputId(string id) {
		return OutputIds.Contains(id);
	}

	public HashSet<string> GetInputIds(){
		return InputIds;
	}
	
	public void AddError(PortCellType cell, string msg){
		Errors.Add($"({cell.X()}, {cell.Y()}). {msg}");
	}
	
	public ProgramContext Parse(OutputCellType cell){
		if(ParsedOutputs.TryGetValue(cell, out ProgramContext output)) return output;
		string expression = cell.GetExpression();
		ProgramContext tree = Utilss.Parse(expression);
		ParsedOutputs.Add(cell, tree);
		return tree;
	}

	public List<string> GetErrors(){
		return Errors;
	}

	public List<CNIPort> ResultInput(){
		List<CNIPort> result = [];
		result.AddRange(Inputs.Select(i => i.ToPort()));
		return result;
	}

	public List<Output> ResultOutput(){
		List<Output> result = [];
		result.AddRange(Outputs.Select(i => i.ToPort(ParsedOutputs[i])));
		return result;
	}
}
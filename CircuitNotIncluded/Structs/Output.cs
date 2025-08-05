using CircuitNotIncluded.Grammar;
using static CircuitNotIncluded.Grammar.ExpressionParser;

namespace CircuitNotIncluded.Structs;
using EvaluateFunc = Func<SymbolTable, int>;

public struct Output {
	public string Expression { get; }
	public CNIPort Port { get; }
	public EvaluateFunc Evaluate { get; }
	public ProgramContext Tree { get; }
	
	public Output(string expression, CNIPort port){
		Expression = expression;
		Port = port;
		Tree = Utilss.Parse(expression);
		Evaluate = ExpressionCompiler.Compile(Tree);
	}

	public Output(string expression, ProgramContext tree, CNIPort port){
		Expression = expression;
		Port = port;
		Tree = tree;
		Evaluate = ExpressionCompiler.Compile(Tree);
	}

	public void Update(SymbolTable table){
		int result = Evaluate(table);
		table.UpdateOutput(Port.P.id, result);
	}
	
}
using CircuitNotIncluded.Grammar;
using CircuitNotIncluded.Grammar.Visitors;
using static CircuitNotIncluded.Grammar.ExpressionParser;

namespace CircuitNotIncluded.Structs;
using EvaluateFunc = Func<SymbolTable, int>;

public struct Output {
	public CNIPort Port { get; }
	public string Expression { get; }
	public EvaluateFunc Evaluate { get; }
	public ProgramContext Tree { get; }
	
	public Output(string expression, CNIPort port){
		Port = port;
		Expression = expression;
		Tree = Compiler.Parse(expression);
		Evaluate = Compiler.Compile(Tree);
	}

	public Output(string expression, ProgramContext tree, CNIPort port){
		Port = port;
		Expression = expression;
		Tree = tree;
		Evaluate = Compiler.Compile(Tree);
	}

	public void Update(SymbolTable table){
		int result = Evaluate(table);
		table.UpdateOutput(Port.P.id, result);
	}
	
}
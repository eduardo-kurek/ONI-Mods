using CircuitNotIncluded.Grammar;

namespace CircuitNotIncluded.Structs;
using EvaluateFunc = Func<SymbolTable, int>;

public struct Output {
	public string Expression { get; }
	public CNIPort Port { get; }
	public EvaluateFunc Evaluate { get; }
	public ExpressionParser.ProgramContext Tree { get; }
	
	public Output(string expression, CNIPort port){
		Expression = expression;
		Port = port ?? throw new ArgumentNullException(nameof(port));
		Tree = Utils.Parse(expression);
		Evaluate = ExpressionCompiler.Compile(Tree);
	}

	public void Update(SymbolTable table){
		int result = Evaluate(table);
		table.UpdateOutput(Port.P.id, result);
	}
	
}
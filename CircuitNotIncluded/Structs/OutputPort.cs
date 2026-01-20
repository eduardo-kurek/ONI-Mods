using CircuitNotIncluded.Grammar;
using static CircuitNotIncluded.Grammar.ExpressionParser;
using static LogicPorts;

namespace CircuitNotIncluded.Structs;
using EvaluateFunc = Func<SymbolTable, int>;

public class OutputPort : CNIPort {
	public string Expression { get; }
	public EvaluateFunc EvaluateFunc { get; }
	public ProgramContext Tree { get; }

	private OutputPort(string expression, ProgramContext tree, string id, CellOffset offset, Port wrappedPort)
		: base(id, wrappedPort)
	{
		Expression = expression;
		Tree = tree;
		EvaluateFunc = Compiler.Compile(Tree);
	}

	public int Evaluate(SymbolTable table) => EvaluateFunc(table);
	
	public static OutputPort Create(string expression, ProgramContext tree, CellOffset offset, PortInfo info){
		var port = Port.OutputPort(
			info.Id, offset, info.Description, info.ActiveDescription, info.InactiveDescription
		);
		return new OutputPort(expression, tree, info.Id, offset, port);
	}
}
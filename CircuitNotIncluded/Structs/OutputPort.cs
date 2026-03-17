using CircuitNotIncluded.Grammar;
using KSerialization;
using static CircuitNotIncluded.Grammar.ExpressionParser;
using static LogicPorts;

namespace CircuitNotIncluded.Structs;
using EvaluateFunc = Func<SymbolTable, int>;

[SerializationConfig(MemberSerialization.OptIn)]
public class OutputPort : CNIPort {
	private string expression;

	[Serialize]
	public string Expression {
		get => expression;
		set {
			if (expression == value) return;
			expression = value;
			tree = null;
			evaluateFunc = null;
		}
	}
	
	private ProgramContext? tree = null;
	private EvaluateFunc? evaluateFunc = null;
	
	public ProgramContext Tree {
		get {
			if (tree == null && !string.IsNullOrEmpty(Expression)) {
				tree = Compiler.Parse(Expression);
			}
			return tree!;
		}
	}
	
	private EvaluateFunc EvaluateFunc {
		get {
			if (evaluateFunc == null) {
				evaluateFunc = Compiler.Compile(Tree);
			}
			return evaluateFunc!;
		}
	}
	
	private OutputPort(){ }

	private OutputPort(string expression, string id, Port wrappedPort)
		: base(id, wrappedPort)
	{
		Expression = expression;
		tree = Compiler.Parse(Expression);
		evaluateFunc = Compiler.Compile(tree);
	}

	public int Evaluate(SymbolTable table) => EvaluateFunc(table);
	
	public static OutputPort Create(string expression, CellOffset offset, PortInfo info){
		var port = Port.OutputPort(
			info.Id, offset, info.Description, info.ActiveDescription, info.InactiveDescription
		);
		return new OutputPort(expression, info.Id, port);
	}
}
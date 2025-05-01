using Antlr4.Runtime.Tree;
using CircuitNotIncluded.Structs;

namespace CircuitNotIncluded.Grammar;


public class SemanticAnalyzer : ExpressionBaseListener {
	private readonly HashSet<string> ids;
	private readonly CellOffset offset;
	private string errors = "";
	private SemanticAnalyzer(HashSet<string> ids, CellOffset offset){
		this.ids = ids;
		this.offset = offset;
	}
	
	public override void ExitIdFactor(ExpressionParser.IdFactorContext context){
		base.ExitIdFactor(context);
		string id = context.ID().GetText();
		if (!ids.Contains(id)){
			errors += $"Input port {id} not found in the circuit.";
		}
	}

	private void ThrowIfErrors(){
		if(errors != "") throw new Exception(errors);
	}
	
	/*
	 * Checks if the input ports called by the expression is present in ids.
	 * Throws an exception if any of the input ports is not found.
	 */
	public static void Analyze(Output output, HashSet<string> ids){
		SemanticAnalyzer semanticAnalyzer = new(ids, output.Port.P.cellOffset);
		ParseTreeWalker.Default.Walk(semanticAnalyzer, output.Tree);
		semanticAnalyzer.ThrowIfErrors();
	}
}
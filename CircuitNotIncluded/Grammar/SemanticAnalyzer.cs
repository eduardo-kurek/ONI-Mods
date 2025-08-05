using Antlr4.Runtime.Tree;
using CircuitNotIncluded.Structs;
using static CircuitNotIncluded.Grammar.ExpressionParser;

namespace CircuitNotIncluded.Grammar;


public class SemanticAnalyzer : ExpressionBaseListener {
	private readonly HashSet<string> ids;
	private string errors = "";
	private SemanticAnalyzer(HashSet<string> ids){
		this.ids = ids;
	}
	
	public override void ExitIdFactor(IdFactorContext context){
		base.ExitIdFactor(context);
		string id = context.ID().GetText();
		if (!ids.Contains(id)){
			errors += $"Input port '{id}' not found in the circuit.\n";
		}
	}

	private void ThrowIfErrors(){
		if(errors != "") throw new Exception($"Semantic errors: \n{string.Join("\n", errors)}");
	}
	
	/*
	 * Checks if the input ports called by the expression is present in ids.
	 * Throws an exception if any of the input ports is not found.
	 */
	public static void Analyze(ProgramContext tree, HashSet<string> ids){
		SemanticAnalyzer semanticAnalyzer = new(ids);
		ParseTreeWalker.Default.Walk(semanticAnalyzer, tree);
		semanticAnalyzer.ThrowIfErrors();
	}
}
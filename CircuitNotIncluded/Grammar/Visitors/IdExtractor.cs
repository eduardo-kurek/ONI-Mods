using Antlr4.Runtime.Tree;
using static CircuitNotIncluded.Grammar.ExpressionParser;

namespace CircuitNotIncluded.Grammar.Visitors;

public class IdExtractor : ExpressionBaseListener {
	private readonly HashSet<string> ids = new();
	private IdExtractor(){ }
	
	public override void ExitIdFactor(IdFactorContext context){
		ids.Add(context.ID().GetText());
	}
	
	public static HashSet<string> Extract(ProgramContext tree){
		var idExtractor = new IdExtractor();
		ParseTreeWalker.Default.Walk(idExtractor, tree);
		return idExtractor.ids;
	}
}
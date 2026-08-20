using Antlr4.Runtime;
using CircuitNotIncluded.Grammar.Visitors;
using CircuitNotIncluded.Grammar.Visitors.Expression;
using static CircuitNotIncluded.Grammar.ExpressionParser;

namespace CircuitNotIncluded.Grammar;

public class Compiler {
	public static ProgramContext Parse(string expression){
		AntlrInputStream inputStream = new(expression);
		ExpressionLexer lexer = new(inputStream);
		CommonTokenStream tokens = new(lexer);
		ExpressionParser parser = new(tokens);
			
		var syntaxAnalyzer = new SyntaxAnalyzer();
		parser.RemoveErrorListeners();
		parser.AddErrorListener(syntaxAnalyzer);
			
		ProgramContext tree = parser.program();
			
		syntaxAnalyzer.ThrowIfErrors();
    
		return tree;
	}
	
	public static void SemanticAnalyze(string expression, HashSet<string> ids) => SemanticAnalyzer.Analyze(expression, ids);
	public static HashSet<string> ExtractIds(ProgramContext tree) => IdExtractor.Extract(tree);
	public static CompiledExpression Compile(string expression) => ExpressionCompiler.Compile(expression);
}
using Antlr4.Runtime;
using CircuitNotIncluded.Grammar.Visitors;
using CircuitNotIncluded.Structs;
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
	
	public static void SemanticAnalyze(ProgramContext tree, HashSet<string> ids) => SemanticAnalyzer.Analyze(tree, ids);
	public static HashSet<string> ExtractIds(ProgramContext tree) => IdExtractor.Extract(tree);
	public static Func<SymbolTable, int> Compile(ProgramContext tree) => ExpressionCompiler.Compile(tree);
}
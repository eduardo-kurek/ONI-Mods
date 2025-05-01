using Antlr4.Runtime;
using static CircuitNotIncluded.Grammar.ExpressionParser;

namespace CircuitNotIncluded.Grammar;

public class SyntaxAnalyzer : BaseErrorListener {
	private List<string> errors = [];
	
	public SyntaxAnalyzer(){ }

	public override void SyntaxError(
		IRecognizer recognizer, 
		IToken offendingSymbol, 
		int line, int charPositionInLine, 
		string msg, RecognitionException e)
	{
		errors.Add($"at char {charPositionInLine}: {msg}");
	}
	
	public void ThrowIfErrors(){
		if(errors.Count > 0){
			throw new Exception($"Syntax errors: {string.Join(";\n", errors)}");
		}
	}
} 
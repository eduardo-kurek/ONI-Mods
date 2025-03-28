using System.Reflection.Emit;
using CircuitNotIncluded.Structs;
using static CircuitNotIncluded.Grammar.ExpressionParser;

namespace CircuitNotIncluded.Grammar;
using EvaluateFunc = Func<SymbolTable, int>;

public class ExpressionCompiler : ExpressionBaseVisitor<object?> {
	private readonly DynamicMethod method;
	private readonly ILGenerator il;
	
	private ExpressionCompiler(){
		method = new DynamicMethod(
			"Temp",
			typeof(int),
			[typeof(SymbolTable)]
		);
		
		il = method.GetILGenerator();
	}

	public override object? VisitIdFactor(IdFactorContext context){
		il.Emit(OpCodes.Ldarg_0);
		il.Emit(OpCodes.Ldstr, context.ID().GetText());
		il.Emit(OpCodes.Call, typeof(SymbolTable).GetMethod("GetInputValue")!);
		return null;
	}

	public override object? VisitOrExpresssion(OrExpresssionContext context){
		Visit(context.expression()[0]);
		Visit(context.expression()[1]);
		il.Emit(OpCodes.Or);
		return null;
	}

	public override object? VisitAndExpresssion(AndExpresssionContext context){
		Visit(context.expression()[0]);
		Visit(context.expression()[1]);
		il.Emit(OpCodes.And);
		return null;
	}
	
	public override object? VisitXorExpresssion(XorExpresssionContext context){
		Visit(context.expression()[0]);
		Visit(context.expression()[1]);
		il.Emit(OpCodes.Xor);
		return null;
	}

	public override object? VisitNotExpresssion(NotExpresssionContext context){
		Visit(context.factor());
		il.Emit(OpCodes.Not);
		return null;
	}

	public override object? VisitProgram(ProgramContext context){
		Visit(context.expression());
		il.Emit(OpCodes.Ret);
		return null;
	}

	public EvaluateFunc GetEvaluateFunc(){
		return (EvaluateFunc)method.CreateDelegate(typeof(EvaluateFunc));
	}

	public static EvaluateFunc Compile(ProgramContext tree){
		ExpressionCompiler compiler = new ExpressionCompiler();
		tree.Accept(compiler);
		return compiler.GetEvaluateFunc();
	}
	
	public static EvaluateFunc Compile(string expression){
		ProgramContext tree = Utils.Parse(expression);
		return Compile(tree);
	}
}
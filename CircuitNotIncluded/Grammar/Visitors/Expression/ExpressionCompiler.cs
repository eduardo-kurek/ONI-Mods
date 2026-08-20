using System.Reflection.Emit;
using static CircuitNotIncluded.Grammar.ExpressionParser;

namespace CircuitNotIncluded.Grammar.Visitors.Expression;
using EvaluateFunc = Func<SymbolTable, ExpressionState, int>;

public class ExpressionCompiler : ExpressionBaseVisitor<object?> {
	private readonly DynamicMethod method;
	private readonly ILGenerator il;
	
	private readonly List<IStatefulGate> gates = [];

	private ExpressionCompiler(){
		method = new DynamicMethod(
			"Temp",
			typeof(int),
			[typeof(SymbolTable), typeof(ExpressionState)]
		);
		
		il = method.GetILGenerator();
	}

	public override object? VisitFilterFunction(FilterFunctionContext context){
		float delayAmount = float.Parse(context.FLOAT().GetText());
		EmitStatefulGateCall(context.expression(), new TransitionDelayState(delayAmount, 0));
		return null;
	}
	
	public override object? VisitBufferFunction(BufferFunctionContext context){
		float delayAmount = float.Parse(context.FLOAT().GetText());
		EmitStatefulGateCall(context.expression(), new TransitionDelayState(delayAmount, 1));
		return null;
	}
	
	private void EmitStatefulGateCall(ExpressionContext expression, IStatefulGate gate){
		gates.Add(gate);
		int index = gates.Count - 1;

		il.Emit(OpCodes.Ldarg_1);
		Visit(expression);
		il.Emit(OpCodes.Ldc_I4, index);
		il.Emit(OpCodes.Callvirt, typeof(ExpressionState).GetMethod("EvaluateGate")!);
	}

	public override object? VisitTrueFactor(TrueFactorContext context){
		il.Emit(OpCodes.Ldc_I4_1);
		return null;
	}
	
	public override object? VisitFalseFactor(FalseFactorContext context){
		il.Emit(OpCodes.Ldc_I4_0);
		return null;
	}

	public override object? VisitIdFactor(IdFactorContext context){
		il.Emit(OpCodes.Ldarg_0);
		il.Emit(OpCodes.Ldstr, context.ID().GetText());
		il.Emit(OpCodes.Call, typeof(SymbolTable).GetMethod("GetValue")!);
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
		il.Emit(OpCodes.Ldc_I4_0);
		il.Emit(OpCodes.Ceq);
		return null;
	}

	public override object? VisitProgram(ProgramContext context){
		if(context.expression() == null){
			il.Emit(OpCodes.Ldc_I4_0);
			il.Emit(OpCodes.Ret);
			return null;
		}
		
		Visit(context.expression());
		il.Emit(OpCodes.Ret);
		return null;
	}

	private EvaluateFunc GetEvaluateFunc(){
		return (EvaluateFunc)method.CreateDelegate(typeof(EvaluateFunc));
	}

	public CompiledExpression GetCompiledExpression(){
		var state = new ExpressionState(gates.ToArray());
		return new CompiledExpression(GetEvaluateFunc(), state);	
	}

	public static CompiledExpression Compile(ProgramContext tree){
		ExpressionCompiler compiler = new ExpressionCompiler();
		tree.Accept(compiler);
		return compiler.GetCompiledExpression();
	}
	
	public static CompiledExpression Compile(string expression){
		ProgramContext tree = Compiler.Parse(expression);
		return Compile(tree);
	}
}
using CircuitNotIncluded.Grammar;
using CircuitNotIncluded.Grammar.Visitors.Expression;
using KSerialization;

namespace CircuitNotIncluded.Core.Runtime;

[SerializationConfig(MemberSerialization.OptIn)]
public class OutputRuntime(SymbolTable symbolTable, string expression, int cell) 
	: PortRuntime(cell), ILogicEventSender
{
	private readonly CompiledExpression compiledExp = Compiler.Compile(expression);
	private int logicValue;
	
	public void OnLogicNetworkConnectionChanged(bool connected){ }
	
	public void LogicTick(){
		logicValue = compiledExp.Evaluate(symbolTable);
	}

	public int GetLogicValue() => logicValue;
	public int GetLogicCell() => base.GetLogicUICell();	
	public override LogicPortSpriteType GetLogicPortSpriteType() => LogicPortSpriteType.Output;
}
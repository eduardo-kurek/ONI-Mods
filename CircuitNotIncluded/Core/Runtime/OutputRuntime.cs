using CircuitNotIncluded.Grammar;
using CircuitNotIncluded.Grammar.Visitors.Expression;
using KSerialization;

namespace CircuitNotIncluded.Core.Runtime;

public class OutputRuntime(SymbolTable symbolTable, CompiledExpression expression, int cell) 
	: PortRuntime(cell), ILogicEventSender
{
	private int logicValue;
	private bool readyToEvaluate;

	public void OnLogicNetworkConnectionChanged(bool connected){
		if (connected)
			readyToEvaluate = false;
	}
	
	public void LogicTick(){
		if (!readyToEvaluate){
			readyToEvaluate = true;
			return;
		}
		logicValue = expression.Evaluate(symbolTable);
	}

	public int GetLogicValue() => logicValue;
	public int GetLogicCell() => base.GetLogicUICell();	
	public override LogicPortSpriteType GetLogicPortSpriteType() => LogicPortSpriteType.Output;
}
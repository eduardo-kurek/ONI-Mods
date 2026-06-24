using CircuitNotIncluded.Grammar;
using KSerialization;

namespace CircuitNotIncluded.Core.Runtime;
using EvaluateFunc = Func<SymbolTable, int>;

[SerializationConfig(MemberSerialization.OptIn)]
public class OutputRuntime(SymbolTable symbolTable, string expression, int cell) 
	: PortRuntime(cell), ILogicEventSender
{
	private readonly EvaluateFunc evaluate = Compiler.Compile(expression);
	private int logicValue;
	
	public void OnLogicNetworkConnectionChanged(bool connected){ }
	
	public void LogicTick(){
		logicValue = evaluate(symbolTable);
	}

	public int GetLogicValue() => logicValue;
	
	public int GetLogicCell() => base.GetLogicUICell();	
	public override LogicPortSpriteType GetLogicPortSpriteType() => LogicPortSpriteType.Output;
}
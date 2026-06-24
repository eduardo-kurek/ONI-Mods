using CircuitNotIncluded.Grammar;
using KSerialization;
using Newtonsoft.Json.Linq;

namespace CircuitNotIncluded.Structs;
using EvaluateFunc = Func<SymbolTable, int>;

[SerializationConfig(MemberSerialization.OptIn)]
public class OutputRuntime(SymbolTable symbolTable, string expression, int cell) 
	: CircuitPort(cell), ILogicEventSender
{
	private readonly EvaluateFunc evaluate = Compiler.Compile(expression);
	private int logicValue;
	
	public void LogicTick(){
		logicValue = evaluate(symbolTable);
	}
	
	public void OnLogicNetworkConnectionChanged(bool connected){ }
	public int GetLogicCell() => cell;
	public int GetLogicValue() => logicValue;
	
	public override LogicPortSpriteType GetLogicPortSpriteType() => LogicPortSpriteType.Output;
}
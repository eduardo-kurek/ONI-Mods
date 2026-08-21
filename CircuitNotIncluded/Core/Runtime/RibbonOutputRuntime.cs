using CircuitNotIncluded.Grammar.Visitors.Expression;

namespace CircuitNotIncluded.Core.Runtime;

public class RibbonOutputRuntime(
	SymbolTable symbolTable,
	CompiledExpression expression1,
	CompiledExpression expression2,
	CompiledExpression expression3,
	CompiledExpression expression4,
	int cell
) : PortRuntime(cell), ILogicEventSender {
	
	private int logicValue;

	public void OnLogicNetworkConnectionChanged(bool connected){ }
	
	public void LogicTick(){
		int v1 = expression1.Evaluate(symbolTable);
		int v2 = expression2.Evaluate(symbolTable);
		int v3 = expression3.Evaluate(symbolTable);
		int v4 = expression4.Evaluate(symbolTable);
		
		logicValue = ((v1 & 1) << 0) |
		             ((v2 & 1) << 1) |
		             ((v3 & 1) << 2) |
		             ((v4 & 1) << 3);
	}
	
	public int GetLogicCell() => base.GetLogicUICell();
	public int GetLogicValue() => logicValue;
	public override LogicPortSpriteType GetLogicPortSpriteType() => LogicPortSpriteType.RibbonOutput;
}
using CircuitNotIncluded.Grammar;

namespace CircuitNotIncluded.Core.Runtime;
using EvaluateFunc = Func<SymbolTable, int>;

public class RibbonOutputRuntime(
	SymbolTable symbolTable,
	string expression1,
	string expression2,
	string expression3,
	string expression4,
	int cell
) : PortRuntime(cell), ILogicEventSender {
	
	private readonly EvaluateFunc evaluate1 = Compiler.Compile(expression1);
	private readonly EvaluateFunc evaluate2 = Compiler.Compile(expression2);
	private readonly EvaluateFunc evaluate3 = Compiler.Compile(expression3);
	private readonly EvaluateFunc evaluate4 = Compiler.Compile(expression4);
	private int logicValue;

	public void OnLogicNetworkConnectionChanged(bool connected){ }
	
	public void LogicTick(){
		int v1 = evaluate1(symbolTable);
		int v2 = evaluate2(symbolTable);
		int v3 = evaluate3(symbolTable);
		int v4 = evaluate4(symbolTable);
		
		logicValue = ((v1 & 1) << 0) |
		             ((v2 & 1) << 1) |
		             ((v3 & 1) << 2) |
		             ((v4 & 1) << 3);
	}
	
	public int GetLogicCell() => base.GetLogicUICell();
	public int GetLogicValue() => logicValue;
	public override LogicPortSpriteType GetLogicPortSpriteType() => LogicPortSpriteType.RibbonOutput;
}
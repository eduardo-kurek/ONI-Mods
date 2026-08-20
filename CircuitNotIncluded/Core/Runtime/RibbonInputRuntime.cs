using CircuitNotIncluded.Grammar.Visitors.Expression;

namespace CircuitNotIncluded.Core.Runtime;

public class RibbonInputRuntime(
	SymbolTable symbolTable,
	string id1,
	string id2,
	string id3,
	string id4,
	int cell
) : PortRuntime(cell), ILogicEventReceiver {

	public void OnLogicNetworkConnectionChanged(bool connected){  }

	public void ReceiveLogicEvent(int value){
		symbolTable.SetValue(id1, (value >> 0) & 1);
		symbolTable.SetValue(id2, (value >> 1) & 1);
		symbolTable.SetValue(id3, (value >> 2) & 1);
		symbolTable.SetValue(id4, (value >> 3) & 1);	
	}

	public int GetLogicCell() => base.GetLogicUICell();
	public override LogicPortSpriteType GetLogicPortSpriteType() => LogicPortSpriteType.RibbonInput;
}
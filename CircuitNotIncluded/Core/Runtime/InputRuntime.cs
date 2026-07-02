using CircuitNotIncluded.Grammar;

namespace CircuitNotIncluded.Core.Runtime;

public class InputRuntime(SymbolTable symbolTable, string id, int cell) 
	: PortRuntime(cell), ILogicEventReceiver
{
	public void OnLogicNetworkConnectionChanged(bool connected){ }
	
	public void ReceiveLogicEvent(int value){
		symbolTable.SetValue(id, value);
	}

	public int GetLogicCell() => base.GetLogicUICell();
	public override LogicPortSpriteType GetLogicPortSpriteType() => LogicPortSpriteType.Input;
}
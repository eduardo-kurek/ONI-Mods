using KSerialization;
using Newtonsoft.Json.Linq;

namespace CircuitNotIncluded.Structs;

public class InputRuntime(SymbolTable symbolTable, string id, int cell) 
	: CircuitPort(cell), ILogicEventReceiver
{
	public void OnLogicNetworkConnectionChanged(bool connected){ }
	
	public void ReceiveLogicEvent(int value){
		symbolTable.SetValue(id, value);
	}
	
	public int GetLogicCell(){
		return cell;
	}
	
	public override LogicPortSpriteType GetLogicPortSpriteType() => LogicPortSpriteType.Input;
}
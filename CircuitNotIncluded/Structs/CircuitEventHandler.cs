using UnityEngine;

namespace CircuitNotIncluded.Structs;

public class CircuitEventHandler(Circuit parent, InputPort inputPort)
	: CircuitPort(parent, inputPort), ILogicEventReceiver
{
	private InputPort inputPort => (InputPort)port;
	
	public void OnLogicNetworkConnectionChanged(bool connected){ }
	
	public void ReceiveLogicEvent(int value){
		parent.OnInputPortChanged(inputPort.OriginalId, value);
	}
	
	public int GetLogicCell(){
		return cell;
	}
	
	public override LogicPortSpriteType GetLogicPortSpriteType() => LogicPortSpriteType.Input;
}
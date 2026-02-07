namespace CircuitNotIncluded.Structs;

public class CircuitInput(Circuit parent, InputPort inputPort)
	: CircuitPort(parent, inputPort), ILogicEventReceiver
{
	public InputPort inputPort => (InputPort)port;
	
	public void OnLogicNetworkConnectionChanged(bool connected){ }
	
	public void ReceiveLogicEvent(int value){
		parent.OnInputPortChanged(inputPort.OriginalId, value);
	}
	
	public int GetLogicCell(){
		return cell;
	}
	
	public override LogicPortSpriteType GetLogicPortSpriteType() => LogicPortSpriteType.Input;
}
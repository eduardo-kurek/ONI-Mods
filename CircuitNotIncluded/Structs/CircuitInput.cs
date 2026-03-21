using KSerialization;
namespace CircuitNotIncluded.Structs;

[SerializationConfig(MemberSerialization.OptIn)]
public class CircuitInput : CircuitPort, ILogicEventReceiver {
	[Serialize] public InputPort port;
	
	private CircuitInput(){ }

	public CircuitInput(Circuit parent, InputPort port) : base(parent, parent.GetActualCell(port.Offset)){
		this.port = port;
	}
	
	public void OnLogicNetworkConnectionChanged(bool connected){ }
	
	public void ReceiveLogicEvent(int value){
		parent.OnInputPortChanged(port.OriginalId, value);
	}
	
	public int GetLogicCell(){
		return cell;
	}
	
	public override LogicPortSpriteType GetLogicPortSpriteType() => LogicPortSpriteType.Input;
}
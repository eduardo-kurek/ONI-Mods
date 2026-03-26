using KSerialization;
using Newtonsoft.Json.Linq;

namespace CircuitNotIncluded.Structs;

[SerializationConfig(MemberSerialization.OptIn)]
public class CircuitOutput : CircuitPort, ILogicEventSender {
	[Serialize] public OutputPort port;
	[Serialize] private int logicValue;
	[Serialize] private bool dirty = true;
	public SymbolTable symbolTable;
	
	private CircuitOutput(){ }

	public CircuitOutput(Circuit parent, OutputPort port, SymbolTable symbolTable)
		: base(parent, parent.GetActualCell(port.Offset))
	{
		this.port = port;
		this.symbolTable = symbolTable;
	}
	
	public void LogicTick(){
		if(!dirty) return;
		logicValue = port.Evaluate(symbolTable);
		dirty = false;
	}

	public void Refresh(){
		dirty = true;
	}
	
	public void OnLogicNetworkConnectionChanged(bool connected){ }
	public int GetLogicCell() => cell;
	public int GetLogicValue() => logicValue;
	
	public override LogicPortSpriteType GetLogicPortSpriteType() 
		=> LogicPortSpriteType.Output;

	public override JObject ToJson(){
		return port.ToJson();
	}
}
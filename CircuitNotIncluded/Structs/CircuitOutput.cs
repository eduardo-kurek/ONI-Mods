namespace CircuitNotIncluded.Structs;

public class CircuitOutput(Circuit circuit, OutputPort outputPort, SymbolTable symbolTable)
	: CircuitPort(circuit, outputPort), ILogicEventSender
{
	public OutputPort outputPort => (OutputPort)port;
	private int logicValue;
	private bool dirty;
	
	public void LogicTick(){
		if(!dirty) return;
		logicValue = outputPort.Evaluate(symbolTable);
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
}
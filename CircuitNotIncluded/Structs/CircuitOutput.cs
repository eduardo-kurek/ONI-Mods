namespace CircuitNotIncluded.Structs;

public class CircuitOutput(Circuit circuit, OutputPort outputPort)
	: CircuitPort(circuit, outputPort), ILogicEventSender
{
	public OutputPort outputPort => (OutputPort)port;
	private int logicValue = 0;
	
	public void OnLogicNetworkConnectionChanged(bool connected){ }

	public void LogicTick(){

	}

	public void Refresh(SymbolTable symbolTable){
		logicValue = outputPort.Evaluate(symbolTable);
	}
	
	public int GetLogicCell() => cell;
	public int GetLogicValue() => logicValue;
	
	public override LogicPortSpriteType GetLogicPortSpriteType() 
		=> LogicPortSpriteType.Output;
}
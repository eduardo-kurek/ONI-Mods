using UnityEngine;

namespace CircuitNotIncluded.Structs;

public class CircuitEventSender : ILogicEventSender, ILogicUIElement {
	private int cell;
	private int logicValue = 0;
	public OutputPort OutputPort { get; }

	public CircuitEventSender(Circuit circuit, OutputPort outputPort){
		OutputPort = outputPort;
		cell = circuit.GetActualCell(outputPort.Offset);
	}

	public void OnLogicNetworkConnectionChanged(bool connected){ }

	public void LogicTick(){

	}

	public void Refresh(SymbolTable symbolTable){
		logicValue = OutputPort.Evaluate(symbolTable);
	}
	
	public void Connect(){
		Game.Instance.logicCircuitSystem.AddToNetworks(cell, this, true);
		Game.Instance.logicCircuitManager.AddVisElem(this);
	}

	public void Disconnect(){
		Game.Instance.logicCircuitSystem.RemoveFromNetworks(cell, this, true);
		Game.Instance.logicCircuitManager.RemoveVisElem(this);
	}
	
	public int GetLogicCell() => cell;
	public int GetLogicValue() => logicValue;
	
	public Vector2 PosMin() => Grid.CellToPos2D(cell);
	public Vector2 PosMax() => Grid.CellToPos2D(cell);
	public int GetLogicUICell() => cell;
	public LogicPortSpriteType GetLogicPortSpriteType() => LogicPortSpriteType.Output;
}
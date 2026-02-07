using UnityEngine;

namespace CircuitNotIncluded.Structs;

public class CircuitEventHandler : ILogicEventReceiver, ILogicUIElement {
	private Circuit circuit;
	private InputPort inputPort;
	private int cell;
	
	public CircuitEventHandler(Circuit circuit, InputPort inputPort){
		this.circuit = circuit;
		this.inputPort = inputPort;
		cell = circuit.GetActualCell(inputPort.Offset);
	}
	
	public void OnLogicNetworkConnectionChanged(bool connected){ }
	
	public void ReceiveLogicEvent(int value){
		circuit.OnInputPortChanged(inputPort.OriginalId, value);
	}
	
	public int GetLogicCell(){
		return cell;
	}

	public void Connect(){
		Game.Instance.logicCircuitSystem.AddToNetworks(cell, this, true);
		Game.Instance.logicCircuitManager.AddVisElem(this);
	}

	public void Disconnect(){
		Game.Instance.logicCircuitSystem.RemoveFromNetworks(cell, this, true);
		Game.Instance.logicCircuitManager.RemoveVisElem(this);
	}

	public Vector2 PosMin() => Grid.CellToPos2D(cell);
	public Vector2 PosMax() => Grid.CellToPos2D(cell);
	public int GetLogicUICell() => cell;
	public LogicPortSpriteType GetLogicPortSpriteType() => LogicPortSpriteType.Input;
}
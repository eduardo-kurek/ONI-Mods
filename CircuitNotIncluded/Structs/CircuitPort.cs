using UnityEngine;

namespace CircuitNotIncluded.Structs;

public abstract class CircuitPort(Circuit parent, CNIPort port) : ILogicUIElement {
	protected readonly Circuit parent = parent;
	protected readonly CNIPort port = port;
	protected readonly int cell = parent.GetActualCell(port.Offset);

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
	public abstract LogicPortSpriteType GetLogicPortSpriteType();
}
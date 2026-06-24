using KSerialization;
using UnityEngine;
using MemberSerialization = KSerialization.MemberSerialization;

namespace CircuitNotIncluded.Core.Runtime;

[SerializationConfig(MemberSerialization.OptIn)]
public abstract class PortRuntime(int cell) : ILogicUIElement {
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
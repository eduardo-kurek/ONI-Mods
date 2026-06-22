using CircuitNotIncluded.Interfaces;
using KSerialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using MemberSerialization = KSerialization.MemberSerialization;

namespace CircuitNotIncluded.Structs;

[SerializationConfig(MemberSerialization.OptIn)]
public abstract class CircuitPort : ILogicUIElement {
	protected int cell;
	
	protected CircuitPort(){ }
	
	protected CircuitPort(int cell){
		this.cell = cell;
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
	public abstract LogicPortSpriteType GetLogicPortSpriteType();
}
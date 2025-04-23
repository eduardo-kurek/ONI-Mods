using UnityEngine;

namespace CircuitNotIncluded.UI.Cells;

public abstract class CircuitCellType {
	protected CircuitCell parent;
	public void SetParent(CircuitCell parent){
		this.parent = parent;
	}
	public abstract GameObject BuildEditorContent();
}
using CircuitNotIncluded.Utils;
using PeterHan.PLib.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CircuitNotIncluded.UI.Cells;

public abstract class CircuitCellState {
	public CircuitCell Owner { get; private set; } = null!;
	
	public abstract GameObject BuildEditorContent();
	public abstract void UpdateCellImage(Image img);

	protected virtual GameObject BuildContainer(){
		GameObject container = new GameObject("Panel")
			.VerticalLayoutGroup()
			.ChildAlignment(TextAnchor.UpperLeft)
			.ChildForceExpandHeight(false)
			.ChildForceExpandWidth(true)
			.Spacing(5)
			.gameObject
			.LayoutElement()
			.FlexibleHeight(1)
			.FlexibleWidth(1)
			.gameObject;
		FieldBuilder.BuildCoordsLabel(container, CellTitle, GetDisplayIndex());
		return container;
	}
	
	public CellOffset GetOffset() {
		return Owner.Offset;
	}

	public int GetDisplayIndex() {
		return Owner.DisplayIndex;
	}
	
	public virtual void OnEnter(CircuitCell owner){
		Owner = owner;
	}
	
	public virtual void OnExit() {}

	protected abstract string CellTitle { get; }
}

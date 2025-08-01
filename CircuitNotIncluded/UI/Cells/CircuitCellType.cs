using CircuitNotIncluded.Utils;
using PeterHan.PLib.UI;
using TMPro;
using UnityEngine;

namespace CircuitNotIncluded.UI.Cells;

public abstract class CircuitCellType(CellOffset offset) {
	protected CellOffset offset = offset;
	protected CircuitCell parent;
	public void SetParent(CircuitCell parent){
		this.parent = parent;
	}
	public abstract GameObject BuildEditorContent();
	
	protected virtual GameObject BuildContainer(){
		GameObject container = new GameObject("Panel")
			.VerticalLayoutGroup()
			.ChildAlignment(TextAnchor.UpperLeft)
			.ChildForceExpandHeight(false)
			.ChildForceExpandWidth(false)
			.Spacing(5)
			.gameObject
			.LayoutElement()
			.FlexibleHeight(1)
			.FlexibleWidth(1)
			.gameObject;
		BuildCoordsLabel(container);
		return container;
	}

	private GameObject BuildCoordsLabel(GameObject container){
		return new PLabel("Coords")
			.Text($"{GetCellTitle()} ({offset.x}, {offset.y})")
			.Style(CircuitCell.TitleStyle)
			.FlexSize(1, 0)
			.AddTo(container);
	}

	protected abstract string GetCellTitle();
	
	protected GameObject BuildTextField(GameObject parent, string labelText, string defaultValue, 
		int maxLength, PUIDelegates.OnTextChanged onTextChanged){
		
		GameObject panel = new PPanel()
			.Direction(PanelDirection.Horizontal)
			.Spacing(5)
			.FlexSize(1, 0)
			.AddTo(parent);

		new PLabel()
			.Text(labelText)
			.Style(CircuitCell.LabelStyle)
			.AddTo(panel);

		new PTextField()
			.Text(defaultValue)
			.Style(CircuitCell.LabelStyle)
			.FlexSize(1, 0)
			.MaxLength(maxLength)
			.TextAlignment(TextAlignmentOptions.Left)
			.SetOnTextChanged(onTextChanged)
			.AddTo(panel);

		return panel;
	}
}
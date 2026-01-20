using CircuitNotIncluded.Utils;
using PeterHan.PLib.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CircuitNotIncluded.UI.Cells;

public abstract class CircuitCellState {
	public CircuitCell Owner { get; private set; } = null!;
	
	public abstract GameObject BuildEditorContent();
	public abstract void UpdateImage(Image img);

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
			.Text($"{GetCellTitle()} ({GetOffset().x}, {GetOffset().y})")
			.Style(CircuitCell.TitleStyle)
			.FlexSize(1, 0)
			.AddTo(container);
	}
	
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

	public CellOffset GetOffset() {
		return Owner.Offset;
	}
	
	public virtual void OnEnter(CircuitCell owner){
		Owner = owner;
	}
	
	public virtual void OnExit() {}

	protected abstract string GetCellTitle();
}

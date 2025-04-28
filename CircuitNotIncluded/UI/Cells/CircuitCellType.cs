using PeterHan.PLib.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CircuitNotIncluded.UI.Cells;

public abstract class CircuitCellType(CellOffset offset) {
	protected CellOffset offset = offset;
	protected CircuitCell parent;
	public void SetParent(CircuitCell parent){
		this.parent = parent;
	}
	public abstract GameObject BuildEditorContent();
	protected virtual GameObject BuildContainer(){
		var panel = new GameObject("Panel");
		var layout = panel.AddComponent<VerticalLayoutGroup>();
		layout.childAlignment = TextAnchor.UpperLeft;
		layout.childForceExpandHeight = false;
		layout.childForceExpandWidth = false;
		layout.spacing = 5;
		panel.AddOrGet<LayoutElement>().flexibleHeight = 1;
		panel.GetComponent<LayoutElement>().flexibleWidth = 1;
		return panel;
	}

	protected static void BuildTextField(GameObject parent, string label, string defaultValue, 
		int maxLength, PUIDelegates.OnTextChanged onTextChanged){
		var panel = new PPanel() {
			Direction = PanelDirection.Horizontal,
			Spacing = 5,
			FlexSize = new Vector2(1, 0)
		};
		
		var pLabel = new PLabel() {
			Text = label,
			TextStyle = CircuitCell.LabelStyle
		}; panel.AddChild(pLabel);

		var text = new PTextField() {
			Text = defaultValue,
			TextStyle = CircuitCell.LabelStyle,
			FlexSize = new Vector2(1, 0),
			MaxLength = maxLength,
			TextAlignment = TextAlignmentOptions.Left,
			OnTextChanged = onTextChanged
		}; panel.AddChild(text);
		panel.AddTo(parent);
	}
}
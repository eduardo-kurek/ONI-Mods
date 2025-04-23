using PeterHan.PLib.UI;
using UnityEngine;
using UnityEngine.UI;

namespace CircuitNotIncluded.UI.Cells;

public class EmptyCellType(string title) : CircuitCellType() {
	public override GameObject BuildEditorContent(){
		var panel = new GameObject("EditorContent");
		panel.AddComponent<VerticalLayoutGroup>();
		panel.AddOrGet<LayoutElement>().flexibleWidth = 1;
		panel.GetComponent<LayoutElement>().flexibleHeight = 1;
		
		var label = new PLabel("Label") {
			Text = "Label: " + title,
			TextStyle = PUITuning.Fonts.TextDarkStyle
		};
		label.AddTo(panel);
		return panel;
	}
}
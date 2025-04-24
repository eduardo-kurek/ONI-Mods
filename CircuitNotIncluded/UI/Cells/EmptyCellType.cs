using PeterHan.PLib.UI;
using UnityEngine;
using UnityEngine.UI;

namespace CircuitNotIncluded.UI.Cells;

public class EmptyCellType(string title) : CircuitCellType {
	protected override GameObject BuildContainer(){
		GameObject container = base.BuildContainer();
		var layout = container.GetComponent<VerticalLayoutGroup>();
		layout.childAlignment = TextAnchor.MiddleCenter;
		layout.spacing = 10;
		return container;
	}
	
	public override GameObject BuildEditorContent(){
		GameObject panel = BuildContainer();
		RectOffset margin = new(10, 10, 10, 10);
		
		var createInput = new PButton() {
			Margin = margin,
			Text = "Create Input Port",
		}; createInput.AddTo(panel);
		
		var createOutput = new PButton() {
			Margin = margin,
			Text = "Create Output Port"
		}; createOutput.AddTo(panel);
		
		return panel;
	}
}
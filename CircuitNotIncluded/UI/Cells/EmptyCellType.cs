using CircuitNotIncluded.Structs;
using PeterHan.PLib.UI;
using UnityEngine;
using UnityEngine.UI;

namespace CircuitNotIncluded.UI.Cells;

public class EmptyCellType(CellOffset offset) : CircuitCellType {
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
			OnClick = (go) => PromoteToInput()
		}; createInput.AddTo(panel);
		
		var createOutput = new PButton() {
			Margin = margin,
			Text = "Create Output Port",
			OnClick = (go) => PromoteToOutput()
		}; createOutput.AddTo(panel);
		
		return panel;
	}

	private void PromoteToInput(){
		CNIPort port = CNIPort.InputPort("id", offset,
			"", "", "");
		InputCellType type = new(port);
		parent.SetCellType(type);
		parent.OnPointerClick(null!);
	}

	private void PromoteToOutput(){
		CNIPort port = CNIPort.InputPort("id", offset,
			"", "", "");
		Output output = new("expression", port);
		OutputCellType type = new(output);
		parent.SetCellType(type);
		parent.OnPointerClick(null!);
	}
}
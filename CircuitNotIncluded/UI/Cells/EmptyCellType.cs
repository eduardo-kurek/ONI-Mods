using CircuitNotIncluded.Structs;
using PeterHan.PLib.UI;
using UnityEngine;
using UnityEngine.UI;

namespace CircuitNotIncluded.UI.Cells;

public class EmptyCellType(CellOffset offset) : CircuitCellType(offset) {
	protected override GameObject BuildContainer(){
		GameObject container = base.BuildContainer();
		var layout = container.GetComponent<VerticalLayoutGroup>();
		layout.spacing = 10;
		return container;
	}
	
	public override GameObject BuildEditorContent(){
		GameObject panel = BuildContainer();
		RectOffset margin = new(10, 10, 10, 10);

		var coords = new PLabel("Coords") {
			Text = $"Empty cell ({offset.x}, {offset.y})",
			TextStyle = CircuitCell.TitleStyle,
			FlexSize = new Vector2(1, 0)
		}; coords.AddTo(panel);

		var buttonPanel = new PPanel() {
			FlexSize = Vector2.one,
			Alignment = TextAnchor.MiddleCenter,
			Spacing = 10
		};
		
		var createInput = new PButton() {
			Margin = margin,
			Text = "Create Input Port",
			OnClick = (go) => PromoteToInput()
		}; buttonPanel.AddChild(createInput);
		
		var createOutput = new PButton() {
			Margin = margin,
			Text = "Create Output Port",
			OnClick = (go) => PromoteToOutput()
		}; buttonPanel.AddChild(createOutput);
		
		buttonPanel.AddTo(panel);
		return panel;
	}

	private void PromoteToInput(){
		InputCellData data = new () { id = "id" };
		InputCellType type = new(data, offset);
		CircuitScreen.InputCellTypes.Add(type);
		parent.SetCellType(type);
		parent.OnPointerClick(null!);
	}

	private void PromoteToOutput(){
		OutputCellData data = new ();
		OutputCellType type = new(data, offset);
		CircuitScreen.OutputCellTypes.Add(type);
		parent.SetCellType(type);
		parent.OnPointerClick(null!);
	}
}
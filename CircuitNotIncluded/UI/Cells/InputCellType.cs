using CircuitNotIncluded.Structs;
using PeterHan.PLib.UI;
using UnityEngine;

namespace CircuitNotIncluded.UI.Cells;

public class InputCellType(CNIPort port) : CircuitCellType {
	private CNIPort port = port;

	public override GameObject BuildEditorContent(){
		var panel = BuildContainer();
		
		var label = new PLabel("Label") {
			Text = "Input, x: " + port.P.cellOffset.x + ", y: " + port.P.cellOffset.y,
			TextStyle = PUITuning.Fonts.TextDarkStyle,
			FlexSize = new Vector2(1, 0)
		}; label.AddTo(panel);

		var deleteButton = new PButton("DeleteButton") {
			Text = "Delete Port",
			Margin = new RectOffset(10, 10, 10, 10),
			OnClick = (go) => Delete()
		}; deleteButton.AddTo(panel); 
		
		return panel;
	}

	public int GetIndex() => CircuitScreen.Instance.Circuit.ToLinearIndex(port);

	private void Delete(){
		EmptyCellType type = new(port.P.cellOffset);
		parent.SetCellType(type);
		parent.OnPointerClick(null!);
	}
}
using CircuitNotIncluded.Structs;
using PeterHan.PLib.UI;
using UnityEngine;

namespace CircuitNotIncluded.UI.Cells;

public class OutputCellType(Output output) : CircuitCellType {
	private readonly Output output = output;
	public override GameObject BuildEditorContent(){
		var panel = BuildContainer();
		
		var label = new PLabel("Label") {
			Text = "Output: ",
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
	public int GetIndex() => CircuitScreen.Instance.Circuit.ToLinearIndex(output.Port);
	
	private void Delete(){
		EmptyCellType type = new(output.Port.P.cellOffset);
		parent.SetCellType(type);
		parent.OnPointerClick(null!);
	}
}
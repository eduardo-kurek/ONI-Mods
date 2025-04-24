using CircuitNotIncluded.Structs;
using PeterHan.PLib.UI;
using UnityEngine;

namespace CircuitNotIncluded.UI.Cells;

public struct OutputCellData(
	string id = "Id",
	string description = "Description",
	string activeDescription = "Active Description",
	string inactiveDescription = "Inactive Description",
	string expression = "")
{
	public string id = id;
	public string description = description;
	public string activeDescription = activeDescription;
	public string inactiveDescription = inactiveDescription;
	public string expression = expression;
}

public class OutputCellType(OutputCellData data, CellOffset offset) : CircuitCellType(offset) {
	private OutputCellData data = data;
	
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
	public int GetIndex() => CircuitScreen.Instance.Circuit.ToLinearIndex(offset);
	
	private void Delete(){
		EmptyCellType type = new(offset);
		parent.SetCellType(type);
		parent.OnPointerClick(null!);
	}

	public static OutputCellType Create(Output output){
		OutputCellData data = new(
			output.Port.OriginalId,
			output.Port.P.description,
			output.Port.P.activeDescription,
			output.Port.P.inactiveDescription,
			output.Expression
		);
		return new OutputCellType(data, output.Port.P.cellOffset);
	}
}
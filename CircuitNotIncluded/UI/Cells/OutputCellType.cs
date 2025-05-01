using CircuitNotIncluded.Structs;
using PeterHan.PLib.UI;
using UnityEngine;

namespace CircuitNotIncluded.UI.Cells;

public struct OutputCellData(
	string description = "Description",
	string activeDescription = "Active Description",
	string inactiveDescription = "Inactive Description",
	string expression = "")
{
	public string description = description;
	public string activeDescription = activeDescription;
	public string inactiveDescription = inactiveDescription;
	public string expression = expression;
}

public class OutputCellType(OutputCellData data, CellOffset offset) : CircuitCellType(offset) {
	private OutputCellData data = data;
	private static int PortCount = 1;

	public static void ResetAutomaticPortIds(){
		PortCount = 1;
	}
	
	public override GameObject BuildEditorContent(){
		var panel = BuildContainer();
		
		var label = new PLabel("Label") {
			Text = "Output: ",
			TextStyle = PUITuning.Fonts.TextDarkStyle,
			FlexSize = new Vector2(1, 0)
		}; label.AddTo(panel);
		
		BuildTextField(panel, "Description: ", data.description, 255, 
			(source, text) => {
			data.description = text;
		});
		
		BuildTextField(panel, "Active Description: ", data.activeDescription, 255, 
			(source, text) => {
			data.activeDescription = text;
		});
		
		BuildTextField(panel, "Inactive Description: ", data.inactiveDescription, 255, 
			(source, text) => {
			data.inactiveDescription = text;
		});
		
		BuildTextField(panel, "Expression: ", data.expression, 255, 
			(source, text) => {
			data.expression = text;
		});
		
		var deleteButton = new PButton("DeleteButton") {
			Text = "Delete Port",
			Margin = new RectOffset(10, 10, 10, 10),
			OnClick = (go) => Delete()
		}; deleteButton.AddTo(panel);
		
		return panel;
	}
	public int GetIndex() => CircuitScreen.Instance.Circuit.ToLinearIndex(offset);
	public string GetExpression() => data.expression.Trim();
	public int X() => offset.x;
	public int Y() => offset.y;
	
	private void Delete(){
		CircuitScreen.OutputCellTypes.Remove(this);
		EmptyCellType type = new(offset);
		parent.SetCellType(type);
		parent.OnPointerClick(null!);
	}

	public Output ToPort(){
		CNIPort port = CNIPort.OutputPort(
			"o" + PortCount++,
			offset,
			data.description,
			data.activeDescription,
			data.inactiveDescription
		);
		return new Output(data.expression.Trim(), port);
	}

	public static OutputCellType Create(Output output){
		OutputCellData data = new(
			output.Port.P.description,
			output.Port.P.activeDescription,
			output.Port.P.inactiveDescription,
			output.Expression
		);
		return new OutputCellType(data, output.Port.P.cellOffset);
	}
}
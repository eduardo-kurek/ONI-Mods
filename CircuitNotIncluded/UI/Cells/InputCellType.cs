using CircuitNotIncluded.Structs;
using PeterHan.PLib.UI;
using TMPro;
using UnityEngine;

namespace CircuitNotIncluded.UI.Cells;

public struct InputCellData(
	string id = "Id", 
	string description = "Description", 
	string activeDescription = "Active Description", 
	string inactiveDescription = "Inactive Description")
{
	public string id = id;
	public string description = description;
	public string activeDescription = activeDescription;
	public string inactiveDescription = inactiveDescription;
}

public class InputCellType(InputCellData data, CellOffset offset) : CircuitCellType(offset) {
	private InputCellData data = data;
	
	public override GameObject BuildEditorContent(){
		var panel = BuildContainer();
		
		var label = new PLabel("Coords") {
			Text = $"Input port ({offset.x}, {offset.y})",
			TextStyle = CircuitCell.TitleStyle,
			FlexSize = new Vector2(1, 0)
		}; label.AddTo(panel);
		
		BuildTextField(panel, "Id: ", data.id, 20);
		BuildTextField(panel, "Description: ", data.description, 255);
		BuildTextField(panel, "Active Description: ", data.activeDescription, 255);
		BuildTextField(panel, "Inactive Description: ", data.inactiveDescription, 255);

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

	public static InputCellType Create(CNIPort port){
		InputCellData data = new(
			port.OriginalId,
			port.P.description,
			port.P.activeDescription,
			port.P.inactiveDescription
		);
		return new InputCellType(data, port.P.cellOffset);
	}
}
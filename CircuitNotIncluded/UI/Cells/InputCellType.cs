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
		
		BuildTextField(panel, "Id: ", data.id, 5, 
		(source, text) => {
			data.id = text;
		});
		
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

		var deleteButton = new PButton("DeleteButton") {
			Text = "Delete Port",
			Margin = new RectOffset(10, 10, 10, 10),
			OnClick = (go) => Delete()
		}; deleteButton.AddTo(panel); 
		
		return panel;
	}

	private void Delete(){
		CircuitScreen.InputCellTypes.Remove(this);
		EmptyCellType type = new(offset);
		parent.SetCellType(type);
		parent.OnPointerClick(null!);
	}
	
	public int GetIndex() => CircuitScreen.Instance.Circuit.ToLinearIndex(offset);
	public string GetId() => data.id;
	public CellOffset GetOffset() => offset;
	
	public CNIPort ToPort(){
		return CNIPort.InputPort(
			data.id, 
			offset, 
			data.description, 
			data.activeDescription, 
			data.inactiveDescription
		);
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
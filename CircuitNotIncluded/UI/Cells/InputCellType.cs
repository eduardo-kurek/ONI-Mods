using CircuitNotIncluded.Structs;
using UnityEngine;

namespace CircuitNotIncluded.UI.Cells;

public class InputCellData(
	string id = "Id",
	string description = "Description",
	string activeDescription = "Active Description",
	string inactiveDescription = "Inactive Description"
) : PortCellData(id, description, activeDescription, inactiveDescription);

public class InputCellType(InputCellData data, CellOffset offset) : PortCellType(data, offset) {
	protected override string GetCellTitle(){ return "Input Port"; }
	
	public override GameObject BuildEditorContent(){
		GameObject panel = BuildContainer();
		BuildIdField(panel);
		BuildDescriptionField(panel);
		BuildActiveDescriptionField(panel);
		BuildInactiveDescription(panel);
		BuildDeleteButton(panel);
		return panel;
	}

	protected override void Delete(){
		CircuitScreen.InputCellTypes.Remove(this);
		base.Delete();
	}
	
	public int GetIndex() => CircuitScreen.Instance.Circuit.ToLinearIndex(offset);
	public string GetId() => data.id.Trim();
	public CellOffset GetOffset() => offset;
	public int X() => offset.x;
	public int Y() => offset.y;
	
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
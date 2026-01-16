using CircuitNotIncluded.Structs;
using UnityEngine;

namespace CircuitNotIncluded.UI.Cells;

public class InputCellData(
	string id = "Id",
	string description = "Description",
	string activeDescription = "Active Description",
	string inactiveDescription = "Inactive Description"
) : PortCellData(id, description, activeDescription, inactiveDescription);

public class InputCellState(InputCellData data, CellOffset offset) : PortCellState(data, offset) {
	protected override string GetCellTitle(){ return "Input Port"; }
	protected override Sprite GetPortSprite(){
		return Assets.instance.logicModeUIData.inputSprite;
	}
	
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
		base.Delete();
	}
	
	public CNIPort ToPort(){
		return CNIPort.InputPort(
			data.id,
			offset,
			data.description, 
			data.activeDescription, 
			data.inactiveDescription
		);
	}

	public static InputCellState Create(CNIPort port){
		InputCellData data = new(
			port.OriginalId,
			port.P.description,
			port.P.activeDescription,
			port.P.inactiveDescription
		);
		return new InputCellState(data, port.P.cellOffset);
	}

	public static InputCellState Create(CellOffset offset){
		return new InputCellState(new InputCellData(), offset);
	}

	public override void OnEnter(CircuitCell owner){
		base.OnEnter(owner);
		CircuitScreenManager.Instance.OnInputCellCreated(this);
	}

	public override void OnExit(){
		CircuitScreenManager.Instance.OnInputCellDeleted(this);
	}
}
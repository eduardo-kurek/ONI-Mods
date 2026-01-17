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
	
	public InputPort ToPort(){
		PortInfo info = new PortInfo(
			data.id,
			offset,
			data.description,
			data.activeDescription,
			data.inactiveDescription
		);
		return InputPort.Create(info);
	}

	public static InputCellState Create(InputPort port){
		InputCellData data = new(
			port.OriginalId,
			port.WrappedPort.description,
			port.WrappedPort.activeDescription,
			port.WrappedPort.inactiveDescription
		);
		return new InputCellState(data, port.WrappedPort.cellOffset);
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
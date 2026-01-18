using CircuitNotIncluded.Structs;
using UnityEngine;

namespace CircuitNotIncluded.UI.Cells;

public class InputCellState(PortInfo info) : PortCellState(info) {
	private readonly PortInfo Info = info;
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
		return InputPort.Create(Info);
	}

	public static InputCellState Create(InputPort port){
		return new InputCellState(port.GetInfo());
	}

	public static InputCellState Create(CellOffset offset){
		PortInfo info = PortInfo.Default(offset);
		return new InputCellState(info);
	}

	public override void OnEnter(CircuitCell owner){
		base.OnEnter(owner);
		CircuitScreenManager.Instance.OnInputCellCreated(this);
	}

	public override void OnExit(){
		CircuitScreenManager.Instance.OnInputCellDeleted(this);
	}
}
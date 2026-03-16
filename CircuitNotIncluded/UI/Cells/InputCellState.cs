using CircuitNotIncluded.Structs;
using CircuitNotIncluded.Utils;
using PeterHan.PLib.UI;
using UnityEngine;

namespace CircuitNotIncluded.UI.Cells;

public class InputCellState(PortInfo info) : PortCellState(info) {
	private readonly PortInfo Info = info;
	protected override string CellTitle => "Input Port";
	protected override Sprite PortSprite => Assets.instance.logicModeUIData.inputSprite;
	
	public override GameObject BuildEditorContent(){
		GameObject panel = BuildContainer();
		BuildIdField(panel);
		BuildDescriptionField(panel);
		BuildActiveDescriptionField(panel);
		BuildInactiveDescription(panel);
		BuildDeleteButton(panel);
		return panel;
	}
	
	public InputPort ToPort(){
		return InputPort.Create(Owner.Offset, Info);
	}

	public static InputCellState Create(InputPort port){
		return new InputCellState(port.GetInfo());
	}

	public static InputCellState Create(){
		PortInfo info = PortInfo.Default();
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
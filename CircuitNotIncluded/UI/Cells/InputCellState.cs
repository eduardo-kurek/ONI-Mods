using CircuitNotIncluded.Structs;
using CircuitNotIncluded.Structs.Ports;
using CircuitNotIncluded.Utils;
using PeterHan.PLib.UI;
using UnityEngine;

namespace CircuitNotIncluded.UI.Cells;

public class InputCellState(string id = "", string description = "") : PortCellState {
	private string Id = id;
	private string Description = description;
	
	protected override string CellTitle => "Input Port";
	protected override Sprite PortSprite => Assets.instance.logicModeUIData.inputSprite;
	public override string GetId() => Id;

	public override GameObject BuildEditorContent(){
		GameObject panel = BuildContainer();
		FieldBuilder.BuildIdField(panel, Id, (_, text) => { Id = text; });
		FieldBuilder.BuildDescriptionField(panel, Description, (_, text) => { Description = text; });
		BuildDeleteButton(panel);
		return panel;
	}
	
	public InputPort ToPort(){
		return new InputPort(Owner.Offset, new InputBit(Id, Description));
	}

	public override void OnEnter(CircuitCell owner){
		base.OnEnter(owner);
		CircuitScreenManager.Instance.OnInputCellCreated(this);
	}

	public override void OnExit(){
		CircuitScreenManager.Instance.OnInputCellDeleted(this);
	}
}
using CircuitNotIncluded.Core.DTO;
using CircuitNotIncluded.UI.Builders;
using UnityEngine;

namespace CircuitNotIncluded.UI.Cells;

public class InputCellState(InputBitDTO inputBitDto) : PortCellState {
	private readonly InputBitForm inputBitForm = new(inputBitDto);
	protected override string CellTitle => "Input Port";
	protected override Sprite PortSprite => Assets.instance.logicModeUIData.inputSprite;
	public override string GetId() => inputBitForm.id;

	public override GameObject BuildEditorContent(){
		GameObject panel = BuildContainer();
		inputBitForm.Build(panel);
		BuildDeleteButton(panel);
		return panel;
	}
	
	public InputPortDTO ToPort() => new(Owner.Offset, inputBitForm.GetValue());

	public override void OnEnter(CircuitCell owner){
		base.OnEnter(owner);
		CircuitScreenManager.Instance.OnInputCellCreated(this);
	}

	public override void OnExit(){
		CircuitScreenManager.Instance.OnInputCellDeleted(this);
	}
}
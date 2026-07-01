using CircuitNotIncluded.Core.DTO;
using CircuitNotIncluded.UI.Builders;
using UnityEngine;

namespace CircuitNotIncluded.UI.Cells;

public class InputCellState(InputBitDTO inputBitDto) : PortCellState {
	private readonly InputBitForm inputBitForm = new(inputBitDto);
	protected override string CellTitle => "Input Port";
	protected override Sprite PortSprite => Assets.instance.logicModeUIData.inputSprite;

	protected override void BuildPortContent(GameObject parent){
		inputBitForm.Build(parent);
	}
	
	public override PortDTO CreateDTO(){
		return new InputPortDTO(Owner.Offset, inputBitForm.GetValue());
	}
}
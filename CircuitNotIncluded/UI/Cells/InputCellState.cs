using CircuitNotIncluded.Core.DTO;
using CircuitNotIncluded.UI.Builders;
using UnityEngine;

namespace CircuitNotIncluded.UI.Cells;

public class InputCellState(InputPortDTO dto) : PortCellState {
	private readonly InputBitForm inputBitForm = new(dto.Bit1);
	
	protected override string CellTitle => "Input Port";
	protected override Sprite PortSprite => Assets.GetSprite("logicInput");

	protected override void BuildPortContent(GameObject parent){
		inputBitForm.Build(parent);
	}
	
	public override PortDTO CreateDTO()
		=> new InputPortDTO(Owner.Offset, inputBitForm.GetValue());
}
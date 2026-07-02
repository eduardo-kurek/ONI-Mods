using CircuitNotIncluded.Core.DTO;
using CircuitNotIncluded.UI.Builders;
using UnityEngine;

namespace CircuitNotIncluded.UI.Cells;

public class OutputCellState(OutputBitDTO outputBitDto) : PortCellState {
	private OutputBitForm outputBitForm = new(outputBitDto);
	protected override string CellTitle => "Output Port";
	protected override Sprite PortSprite => Assets.instance.logicModeUIData.outputSprite;
	
	protected override void BuildPortContent(GameObject parent){
		outputBitForm.Build(parent);
	}

	public override PortDTO CreateDTO(){
		return new OutputPortDTO(GetOffset(), outputBitForm.GetValue());
	}
}
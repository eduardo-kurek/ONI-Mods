using CircuitNotIncluded.Core.DTO;
using CircuitNotIncluded.UI.Builders;
using UnityEngine;

namespace CircuitNotIncluded.UI.Cells;

public class RibbonOutputCellState(RibbonOutputDTO dto) : PortCellState {
	private readonly OutputBitForm form1 = new(dto.Bit1);
	private readonly OutputBitForm form2 = new(dto.Bit2);
	private readonly OutputBitForm form3 = new(dto.Bit3);
	private readonly OutputBitForm form4 = new(dto.Bit4);
	
	protected override string CellTitle => "Ribbon Output Port";
	protected override Sprite PortSprite => Assets.GetSprite("logic_ribbon_all_out");
	
	protected override void BuildPortContent(GameObject parent){
		FieldBuilder.BuildBitLabel(parent, 1);
		form1.Build(parent);
		FieldBuilder.BuildBitLabel(parent, 2);
		form2.Build(parent);
		FieldBuilder.BuildBitLabel(parent, 3);
		form3.Build(parent);
		FieldBuilder.BuildBitLabel(parent, 4);
		form4.Build(parent);
	}
	
	public override PortDTO CreateDTO()
		=> new RibbonOutputDTO(Owner.Offset, form1.GetValue(), form2.GetValue(), form3.GetValue(), form4.GetValue());
	
}
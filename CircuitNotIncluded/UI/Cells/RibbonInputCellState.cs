using CircuitNotIncluded.Core.DTO;
using CircuitNotIncluded.UI.Builders;
using CircuitNotIncluded.Utils;
using PeterHan.PLib.UI;
using UnityEngine;

namespace CircuitNotIncluded.UI.Cells;

public class RibbonInputCellState(RibbonInputDTO dto) : PortCellState {
	private readonly InputBitForm form1 = new(dto.Bit1);
	private readonly InputBitForm form2 = new(dto.Bit2);
	private readonly InputBitForm form3 = new(dto.Bit3);
	private readonly InputBitForm form4 = new(dto.Bit4);
	
	protected override string CellTitle => "Ribbon Input Port";
	protected override Sprite PortSprite => Assets.GetSprite("logic_ribbon_all_in");
	
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
		=> new RibbonInputDTO(Owner.Offset, form1.GetValue(), form2.GetValue(), form3.GetValue(), form4.GetValue());
	
}
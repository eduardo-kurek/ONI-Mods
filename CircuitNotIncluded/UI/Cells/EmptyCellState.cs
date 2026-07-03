using CircuitNotIncluded.Core.DTO;
using CircuitNotIncluded.UI.Builders;
using CircuitNotIncluded.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace CircuitNotIncluded.UI.Cells;

public class EmptyCellState : CircuitCellState {
	protected override string CellTitle => "Empty Cell";

	public override void UpdateCellImage(Image img){
		img.color = new Color(42/255f, 81/255f, 125/255f);
	}

	protected override GameObject BuildContainer(){
		return base.BuildContainer()
			.VerticalLayoutGroup()
			.Spacing(10)
			.gameObject;
	}
	
	public override GameObject BuildEditorContent(){
		GameObject mainPanel = BuildContainer();
		GameObject buttonsPanel = FieldBuilder.BuildButtonsPanel(mainPanel);
		FieldBuilder.BuildButton(buttonsPanel, "Create Input Port", (go) => PromoteToInput());
		FieldBuilder.BuildButton(buttonsPanel, "Create Ribbon Input Port", (go) => PromoteToRibbonInput());
		FieldBuilder.BuildButton(buttonsPanel, "Create Output Port", (go) => PromoteToOutput());
		FieldBuilder.BuildButton(buttonsPanel, "Create Ribbon Output Port", (go) => PromoteToRibbonOutput());
		return mainPanel;
	}

	private void PromoteToInput(){
		var inputType = new InputCellState(
			new InputPortDTO(
				Owner.Offset, 
				new InputBitDTO("", "")
			)
		);
		Owner.TransitionTo(inputType);
	}
	
	private void PromoteToRibbonInput(){
		var ribbonInputType = new RibbonInputCellState(
			new RibbonInputDTO(
				Owner.Offset, 
				new InputBitDTO("", ""),
				new InputBitDTO("", ""),
				new InputBitDTO("", ""),
				new InputBitDTO("", "")
			)
		);
		Owner.TransitionTo(ribbonInputType);
	}

	private void PromoteToOutput(){
		var outputType = new OutputCellState(new OutputBitDTO("", "", ""));
		Owner.TransitionTo(outputType);
	}
	
	private void PromoteToRibbonOutput(){
		var ribbonOutputType = new RibbonOutputCellState(
			new RibbonOutputDTO(
				Owner.Offset, 
				new OutputBitDTO("", "", ""),
				new OutputBitDTO("", "", ""),
				new OutputBitDTO("", "", ""),
				new OutputBitDTO("", "", "")
			)
		);
		Owner.TransitionTo(ribbonOutputType);
	}
}
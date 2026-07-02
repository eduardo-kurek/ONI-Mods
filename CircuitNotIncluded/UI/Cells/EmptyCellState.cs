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
		FieldBuilder.BuildButton(buttonsPanel, "Create Output Port", (go) => PromoteToOutput());
		return mainPanel;
	}

	private void PromoteToInput(){
		var inputType = new InputCellState(new InputBitDTO("", ""));
		Owner.TransitionTo(inputType);
	}

	private void PromoteToOutput(){
		var outputType = new OutputCellState(new OutputBitDTO("", "", ""));
		Owner.TransitionTo(outputType);
	}
}
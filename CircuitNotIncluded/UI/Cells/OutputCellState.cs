using CircuitNotIncluded.Structs.Ports;
using CircuitNotIncluded.UI.Builders;
using UnityEngine;

namespace CircuitNotIncluded.UI.Cells;

public class OutputCellState(OutputBit outputBit) : PortCellState {
	private OutputBitForm outputBitForm = new(outputBit);

	protected override string CellTitle => "Output Port";
	protected override Sprite PortSprite => Assets.instance.logicModeUIData.outputSprite;
	public override string GetId() => outputBitForm.label.Trim();

	public override GameObject BuildEditorContent(){
		GameObject panel = BuildContainer();
		outputBitForm.Build(panel);
		BuildDeleteButton(panel);
		return panel;
	}

	public string GetExpression() => outputBitForm.expression.Trim();
	
	public OutputPort ToPort() => new(GetOffset(), outputBitForm.GetValue());

	public override void OnEnter(CircuitCell owner){
		base.OnEnter(owner);
		CircuitScreenManager.Instance.OnOutputCellCreated(this);
	}
	
	public override void OnExit(){
		CircuitScreenManager.Instance.OnOutputCellDeleted(this);
	}
}
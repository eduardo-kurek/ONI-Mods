using CircuitNotIncluded.Structs;
using CircuitNotIncluded.Structs.Ports;
using CircuitNotIncluded.Utils;
using UnityEngine;

namespace CircuitNotIncluded.UI.Cells;


public class OutputCellState(string label = "", string description = "", string expression = "") : PortCellState {
	private string Label = label;
	private string Description = description;
	private string Expression = expression;

	protected override string CellTitle => "Output Port";
	protected override Sprite PortSprite => Assets.instance.logicModeUIData.outputSprite;
	public override string GetId() => Label.Trim();

	public override GameObject BuildEditorContent(){
		GameObject panel = BuildContainer();
		FieldBuilder.BuildLabelField(panel, Label, (_, t) => Label = t);
		FieldBuilder.BuildDescriptionField(panel, Description, (_, t) => Description = t);
		FieldBuilder.BuildExpressionField(panel, Expression, (_, t) => Expression = t);
		BuildDeleteButton(panel);
		return panel;
	}

	public string GetExpression() => Expression.Trim();
	
	public OutputPortDef ToPort(){
		return new OutputPortDef(GetOffset(), new OutputBit(Label, Description, Expression));
	}

	public override void OnEnter(CircuitCell owner){
		base.OnEnter(owner);
		CircuitScreenManager.Instance.OnOutputCellCreated(this);
	}
	
	public override void OnExit(){
		CircuitScreenManager.Instance.OnOutputCellDeleted(this);
	}
}
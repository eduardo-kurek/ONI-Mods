using CircuitNotIncluded.Structs;
using UnityEngine;
using static CircuitNotIncluded.Grammar.ExpressionParser;

namespace CircuitNotIncluded.UI.Cells;


public class OutputCellState(string expression, PortInfo info) : PortCellState(info) {
	private string Expression = expression;
	private readonly PortInfo Info = info;

	protected override string GetCellTitle(){ return "Output Port"; }
	protected override Sprite GetPortSprite(){
		return Assets.instance.logicModeUIData.outputSprite;
	}
	
	public override GameObject BuildEditorContent(){
		GameObject panel = BuildContainer();
		BuildIdField(panel);
		BuildDescriptionField(panel);
		BuildActiveDescriptionField(panel);
		BuildInactiveDescription(panel);
		BuildExpressionField(panel);
		BuildDeleteButton(panel);
		return panel;
	}

	private GameObject BuildExpressionField(GameObject container){
		return BuildTextField(container, "Expression: ", Expression, 255, 
			(source, text) => {
				Expression = text;
			});
	}
	
	public string GetExpression() => Expression.Trim();
	
	public OutputPort ToPort(ProgramContext tree){
		return OutputPort.Create(Expression, tree, Owner.Offset, Info);
	}

	public static OutputCellState Create(OutputPort outputPort){
		return new OutputCellState(outputPort.Expression, outputPort.GetInfo());
	}
	
	public static OutputCellState Create(){
		PortInfo info = PortInfo.Default();
		return new OutputCellState(string.Empty, info);
	}

	public override void OnEnter(CircuitCell owner){
		base.OnEnter(owner);
		CircuitScreenManager.Instance.OnOutputCellCreated(this);
	}
	
	public override void OnExit(){
		CircuitScreenManager.Instance.OnOutputCellDeleted(this);
	}
}
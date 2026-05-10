using CircuitNotIncluded.Structs;
using UnityEngine;

namespace CircuitNotIncluded.UI.Cells;


public class OutputCellState(string expression, PortInfo info) : PortCellState {
	private string Expression = expression;
	private readonly PortInfo Info = info;
	protected override string CellTitle => "Output Port";
	protected override Sprite PortSprite => Assets.instance.logicModeUIData.outputSprite;
	public override string GetId() => Info.Id.Trim();
	
	public override bool Equals(object other){
		if(other is OutputCellState o){
			return Info.Equals(o.Info);
		}
		return false;
	}

	public override int GetHashCode() => Info.GetHashCode();
	
	public OutputCellState Clone() => new(Expression, info with {});

	public override GameObject BuildEditorContent(){
		GameObject panel = BuildContainer();
		BuildIdField(panel, "Id :", info);
		BuildDescriptionField(panel,"Description :", info);
		BuildActiveDescriptionField(panel, info);
		BuildInactiveDescription(panel, info);
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
	
	public OutputPort ToPort(){
		return OutputPort.Create(Expression, Owner.Offset, Info);
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
using CircuitNotIncluded.Structs;
using UnityEngine;
using static CircuitNotIncluded.Grammar.ExpressionParser;

namespace CircuitNotIncluded.UI.Cells;

public class OutputCellData(
	string id = "Id",
	string description = "Description",
	string activeDescription = "Active Description",
	string inactiveDescription = "Inactive Description",
	string expression = ""
) : PortCellData(id, description, activeDescription, inactiveDescription)
{
	public string expression = expression;
}

public class OutputCellState(OutputCellData data, CellOffset offset) : PortCellState(data, offset) {
	
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
		return BuildTextField(container, "Expression: ", data.expression, 255, 
			(source, text) => {
				data.expression = text;
			});
	}
	
	public string GetExpression() => data.expression.Trim();
	public int X => offset.x;
	public int Y => offset.y;
	
	public OutputPort ToPort(ProgramContext tree){
		PortInfo info = new PortInfo(
			data.id,
			offset,
			data.description,
			data.activeDescription,
			data.inactiveDescription
		);
		return OutputPort.Create(data.expression.Trim(), tree, info);
	}

	public static OutputCellState Create(OutputPort outputPort){
		OutputCellData data = new(
			outputPort.OriginalId,
			outputPort.WrappedPort.description,
			outputPort.WrappedPort.activeDescription,
			outputPort.WrappedPort.inactiveDescription,
			outputPort.Expression
		);
		return new OutputCellState(data, outputPort.WrappedPort.cellOffset);
	}
	
	public static OutputCellState Create(CellOffset offset){
		return new OutputCellState(new OutputCellData(), offset);
	}

	public override void OnEnter(CircuitCell owner){
		base.OnEnter(owner);
		CircuitScreenManager.Instance.OnOutputCellCreated(this);
	}
	
	public override void OnExit(){
		CircuitScreenManager.Instance.OnOutputCellDeleted(this);
	}
}
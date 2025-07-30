using CircuitNotIncluded.Structs;
using UnityEngine;

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

public class OutputCellType(OutputCellData data, CellOffset offset) : PortCellType(data, offset) {
	protected override string GetCellTitle(){ return "Output Port"; }
	
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
	
	public int GetIndex() => CircuitScreen.Instance.Circuit.ToLinearIndex(offset);
	public string GetExpression() => data.expression.Trim();
	public int X() => offset.x;
	public int Y() => offset.y;
	
	protected override void Delete(){
		CircuitScreen.OutputCellTypes.Remove(this);
		base.Delete();
	}

	public Output ToPort(){
		CNIPort port = CNIPort.OutputPort(
			data.id,
			offset,
			data.description,
			data.activeDescription,
			data.inactiveDescription
		);
		return new Output(data.expression.Trim(), port);
	}

	public static OutputCellType Create(Output output){
		OutputCellData data = new(
			output.Port.OriginalId,
			output.Port.P.description,
			output.Port.P.activeDescription,
			output.Port.P.inactiveDescription,
			output.Expression
		);
		return new OutputCellType(data, output.Port.P.cellOffset);
	}
}
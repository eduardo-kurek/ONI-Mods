using CircuitNotIncluded.UI.Cells;
using PeterHan.PLib.UI;
using TMPro;
using UnityEngine;

namespace CircuitNotIncluded.Utils;

public static class FieldBuilder {
	
	public static GameObject BuildCoordsLabel(GameObject container, string title, int index){
		return new PLabel("Coords")
			.Text($"{title} ({index})")
			.Style(CircuitCell.TitleStyle)
			.FlexSize(1, 0)
			.AddTo(container);
	}
	
	public static GameObject BuildTextField(GameObject parent, string labelText, string defaultValue, 
		int maxLength, TextStyleSetting fieldStyle, PUIDelegates.OnTextChanged onTextChanged){
		
		GameObject panel = new PPanel()
			.Direction(PanelDirection.Horizontal)
			.Spacing(5)
			.FlexSize(1, 0)
			.AddTo(parent);

		new PLabel()
			.Text(labelText)
			.Style(CircuitCell.LabelStyle)
			.AddTo(panel);

		new PTextField()
			.Text(defaultValue)
			.Style(fieldStyle)
			.FlexSize(1, 0)
			.MaxLength(maxLength)
			.TextAlignment(TextAlignmentOptions.Left)
			.SetOnTextChanged(onTextChanged)
			.AddTo(panel);

		return panel;
	}
	
	public static GameObject BuildIdField(GameObject container, string defaultValue, PUIDelegates.OnTextChanged onTextChanged) {
		return BuildTextField(container, "Id: ", defaultValue, 50, CircuitCell.ExpressionStyle, onTextChanged);
	}
	
	public static GameObject BuildLabelField(GameObject container, string defaultValue, PUIDelegates.OnTextChanged onTextChanged) {
		return BuildTextField(container, "Label: ", defaultValue, 50, CircuitCell.ExpressionStyle, onTextChanged);
	}
	
	public static GameObject BuildDescriptionField(GameObject container, string defaultValue, PUIDelegates.OnTextChanged onTextChanged){
		return BuildTextField(container, "Description: ", defaultValue, 255, CircuitCell.LabelStyle, onTextChanged);
	}
	
	public static GameObject BuildExpressionField(GameObject container, string defaultValue, PUIDelegates.OnTextChanged onTextChanged){
		return BuildTextField(container, "Expression: ", defaultValue, 255, CircuitCell.ExpressionStyle, onTextChanged);
	}	
}
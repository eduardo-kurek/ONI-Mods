using CircuitNotIncluded.UI.Cells;
using CircuitNotIncluded.Utils;
using PeterHan.PLib.UI;
using TMPro;
using UnityEngine;
using static PeterHan.PLib.UI.PUIDelegates;

namespace CircuitNotIncluded.UI.Builders;

public static class FieldBuilder {
	
	public static GameObject BuildCoordsLabel(GameObject container, string title, int index){
		return new PLabel("Coords")
			.Text($"{title} ({index})")
			.Style(CircuitCell.TitleStyle)
			.FlexSize(1, 0)
			.AddTo(container);
	}
	
	public static GameObject BuildTextField(GameObject parent, string labelText, string defaultValue, 
		int maxLength, TextStyleSetting fieldStyle, OnTextChanged onTextChanged){
		
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
	
	public static GameObject BuildButtonsPanel(GameObject container){
		return new PPanel()
			.FlexSize(1, 1)
			.Spacing(10)
			.Alignment(TextAnchor.MiddleCenter)
			.AddTo(container);
	}

	public static GameObject BuildButton(GameObject container, string text, PUIDelegates.OnButtonPressed onClick){
		return new PButton()
			.Text(text)
			.Margin(10)
			.SetOnClick(onClick)
			.AddTo(container);
	}

	public static GameObject BuildDeleteButton(GameObject container, OnButtonPressed onButtonPressed){
		GameObject deletePanel = new PPanel("DeletePanel")
			.Direction(PanelDirection.Horizontal)
			.Alignment(TextAnchor.MiddleCenter)
			.Margin(20, 0, 0, 0)
			.AddTo(container);
		
		return new PButton("DeleteButton")
			.Text("Delete Port")
			.Margin(10)
			.SetOnClick(onButtonPressed)
			.AddTo(deletePanel);
	}
	
	public static GameObject BuildIdField(GameObject container, string defaultValue, OnTextChanged onTextChanged) {
		return BuildTextField(container, "Id: ", defaultValue, 50, CircuitCell.ExpressionStyle, onTextChanged);
	}
	
	public static GameObject BuildLabelField(GameObject container, string defaultValue, OnTextChanged onTextChanged) {
		return BuildTextField(container, "Label: ", defaultValue, 50, CircuitCell.ExpressionStyle, onTextChanged);
	}
	
	public static GameObject BuildDescriptionField(GameObject container, string defaultValue, OnTextChanged onTextChanged){
		return BuildTextField(container, "Description: ", defaultValue, 255, CircuitCell.LabelStyle, onTextChanged);
	}
	
	public static GameObject BuildExpressionField(GameObject container, string defaultValue, OnTextChanged onTextChanged){
		return BuildTextField(container, "Expression: ", defaultValue, 255, CircuitCell.ExpressionStyle, onTextChanged);
	}	
}
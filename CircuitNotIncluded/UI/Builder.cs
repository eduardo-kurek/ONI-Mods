using PeterHan.PLib.UI;
using TMPro;
using UnityEngine;

namespace CircuitNotIncluded.UI;

public class Builder
{
	public static GameObject LightText(string message, Transform? parent = null){
		return Text(message, PUITuning.Fonts.UILightStyle, parent);
	}

	public static GameObject DarkText(string message, Transform? parent = null){
		return Text(message, PUITuning.Fonts.UIDarkStyle, parent);
	}
	
	public static GameObject BlueText(string message, Transform? parent = null){
		var text = Text(message, PUITuning.Fonts.UIDarkStyle, parent);
		var textMesh = text.GetComponent<TextMeshProUGUI>();
		textMesh.color = PUITuning.Colors.ButtonBlueStyle.inactiveColor;
		return text;
	}
	
	public static GameObject PinkText(string message, Transform? parent = null){
		var text = Text(message, PUITuning.Fonts.UIDarkStyle, parent);
		var textMesh = text.GetComponent<TextMeshProUGUI>();
		textMesh.color = PUITuning.Colors.ButtonPinkStyle.inactiveColor;
		return text;
	}
	
	public static GameObject Text(string message, TextStyleSetting style, Transform? parent = null){
		var label = new GameObject();
		var text = label.AddComponent<TextMeshProUGUI>();
		text.text = message;
		text.fontSize = style.fontSize;
		text.fontStyle = style.style;
		text.font = style.sdfFont;
		text.color = style.textColor;
		if(parent != null)
			label.transform.SetParent(parent);
		return label;
	}
}
using CircuitNotIncluded.Structs;
using PeterHan.PLib.Core;
using PeterHan.PLib.UI;
using UnityEngine;
using UnityEngine.UI;

namespace CircuitNotIncluded.UI;

public class CircuitScreen : KModalScreen {
	private static GameObject? parent;
	private LocText title;
	
	private static GameObject GetParent(){
		if(parent == null)
			parent = GameObject.Find("MiddleCenter");
		return parent;
	}
	
	public static GameObject Build(Circuit circuit){
		var go = new GameObject("CircuitScreen");
		go.AddComponent<CircuitScreen>();
		var rt = go.AddComponent<RectTransform>();
		rt.anchorMin = Vector2.zero;
		rt.anchorMax = Vector2.one;
		rt.offsetMin = Vector2.zero;
		rt.offsetMax = Vector2.zero;
		go.SetParent(GetParent());
		return go;
	}

	protected override void OnPrefabInit(){
		base.OnPrefabInit();
		PPanel contentPanel = new PPanel();
		contentPanel.SetKleiBlueColor();
		contentPanel.DynamicSize = true;
		contentPanel.FlexSize = Vector2.one;

		GameObject content = contentPanel.Build();
		var outline = content.AddComponent<Outline>();
		outline.effectColor = Color.black;
      outline.effectDistance = new Vector2(1, 1);
      
		var rect = content.GetComponent<RectTransform>();	
		rect.sizeDelta = new Vector2(1100, 700);
		rect.anchorMin = new Vector2(0.5f, 0.5f);
		rect.anchorMax = new Vector2(0.5f, 0.5f);
		rect.localPosition = Vector3.zero;
		rect.localScale = Vector3.one;

		BuildHeader(content);
		BuildBody(content);
		BuildFooter(content);
		
		content.SetParent(gameObject);
	}

	private void BuildHeader(GameObject container){
		var textStyle = PUITuning.Fonts.UILightStyle; 
		textStyle.fontSize = 15;
		
		var header = new PPanel("Header");
		var title = new PLabel("Title") {
			Text = "Circuit name",
			Margin = new RectOffset(10, 10, 10, 10),
			TextStyle = textStyle
		};
		header.SetKleiPinkColor();
		header.AddChild(title);
		
		var obj = header.AddTo(container);
		AddBorder(obj);
		var outline = obj.AddComponent<Outline>();
		outline.effectColor = Color.black;
		outline.effectDistance = new Vector2(1, 1);
		
		var layout = obj.AddComponent<LayoutElement>();
		layout.flexibleWidth = 1;
		layout.preferredHeight = 20;
		
		this.title = obj.GetComponentInChildren<LocText>();
	}

	private void BuildBody(GameObject container){
		var body = new PPanel("Body"){
			Direction = PanelDirection.Horizontal,
			BackColor = new Color32(28, 32, 38, byte.MaxValue)
		};
		
		var displayTab = new PPanel("Circuit"){
			Alignment = TextAnchor.MiddleCenter
		}; body.AddChild(displayTab);

		var editorTab = new PPanel("Editor"){
			Direction = PanelDirection.Vertical,
			Spacing = 5,
			Margin = new RectOffset(10, 10, 10, 10),
			BackColor = Color.white
		}; body.AddChild(editorTab);
		
		var obj = body.AddTo(container);
		AddBorder(obj);
		obj.AddOrGet<LayoutElement>().flexibleHeight = 1;
		obj.AddOrGet<LayoutElement>().flexibleWidth = 1;

		var display = obj.transform.GetChild(0).gameObject;
		AddBorder(display);
		display.AddOrGet<LayoutElement>().flexibleWidth = 1;
		display.AddOrGet<LayoutElement>().flexibleHeight = 1;
		
		var editor = obj.transform.GetChild(1).gameObject;
		AddBorder(editor);
		editor.AddOrGet<LayoutElement>().preferredWidth = 300;
		editor.AddOrGet<LayoutElement>().flexibleHeight = 1;
	}

	private void BuildFooter(GameObject container){
		var titleBar = new PPanel("Footer"){
			Spacing = 15,
			Direction = PanelDirection.Horizontal,
			Margin = new RectOffset(10, 10, 10,10)
		};
		
		var textStyle = PUITuning.Fonts.UILightStyle; 
		textStyle.fontSize = 15;
		
		var saveButton = new PButton(){
			Text = "Save",
			TextStyle = textStyle,
			OnClick = (go) => { SaveButtonClicked(); },
			Margin = new RectOffset(10, 10, 10 ,10)
		};
		
		var cancelButton = new PButton(){
			Text = "Cancel",
			TextStyle = textStyle,
			OnClick = (go) => { CancelButtonClicked(); },
			Margin = new RectOffset(10, 10, 10 ,10)
		};

		titleBar.AddChild(saveButton);
		titleBar.AddChild(cancelButton);

		var obj = titleBar.AddTo(container);
		AddBorder(obj);
		
		var layout = obj.AddOrGet<LayoutElement>();
		layout.flexibleWidth = 1;
	}

	private static void AddBorder(GameObject go){
		var outline = go.AddComponent<Outline>();
		outline.effectColor = Color.black;
		outline.effectDistance = new Vector2(1, 1);
	}
	
	private void SaveButtonClicked(){
		Debug.Log("Saving state");
		Deactivate();
	}

	private void CancelButtonClicked(){
		Debug.Log("Canceling state");
		Deactivate();
	}
}
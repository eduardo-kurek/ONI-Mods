using System.Text.RegularExpressions;
using CircuitNotIncluded.Structs;
using PeterHan.PLib.Core;
using PeterHan.PLib.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CircuitNotIncluded.UI;

public class CircuitSideScreen : SideScreenContent {
	private Circuit? circuit = null;
	private TextMeshProUGUI circuitName = null!;
	private GameObject outputsPanel = null!;
	private Color specialCharsColor = new Color(0.5294118f, 0.2724914f, 0.4009516f);
	private bool builded = false;
	
	protected override void OnPrefabInit(){
		base.OnPrefabInit();
		Build();
		Refresh();
	}

	private void Build(){
		BuildContentContainer();
		BuildCircuitName();
		BuildOutputsLabel();
		BuildOutputsPanel();
		builded = true;
	}

	private void BuildContentContainer(){
		ContentContainer = new GameObject("Content");
		ContentContainer.SetParent(gameObject);
		var layout = ContentContainer.AddComponent<VerticalLayoutGroup>();
		layout.spacing = 5;
		layout.padding = new RectOffset(10, 10, 10, 10);
		layout.childAlignment = TextAnchor.MiddleCenter;
		
		// Margin of 10 px each side
		var rt = ContentContainer.GetComponent<RectTransform>();
		rt.offsetMax = new Vector2(10, 10);
		rt.offsetMin = new Vector2(10, 10);
	}

	private void BuildCircuitName(){
		var layout = ContentContainer.GetComponent<VerticalLayoutGroup>().transform;
		var go = Builder.PinkText("", layout);
		circuitName = go.GetComponent<TextMeshProUGUI>();
		circuitName.fontSize = 16;
		circuitName.alignment = TextAlignmentOptions.Center;
	}

	private void BuildOutputsPanel(){
		outputsPanel = new GameObject("Outputs");
		var parent = ContentContainer.GetComponent<VerticalLayoutGroup>();
		outputsPanel.transform.SetParent(parent.transform);
		var layout = outputsPanel.AddComponent<VerticalLayoutGroup>();
		layout.spacing = 5;
		layout.childAlignment = TextAnchor.UpperLeft;
	}

	private void BuildOutputsLabel(){
		var layout = ContentContainer.GetComponent<VerticalLayoutGroup>().transform;
		Builder.DarkText("Outputs", layout);
	}
	
	public override void SetTarget(GameObject target){
		circuit = target.GetComponent<Circuit>();
		Refresh();
	}
	
	private void Refresh(){
		if(circuit == null) return;
		if(!builded) return;
		UpdateData();
	}

	private void UpdateData(){
		circuitName.text = circuit!.GetCNIName();
		UpdateOutputs();
	}

	private void UpdateOutputs(){
		ClearOutputs();
		CreateOutputs();
	}
	
	private void ClearOutputs(){
		var layout = outputsPanel.GetComponent<VerticalLayoutGroup>();
		foreach(Transform child in layout.transform){
			Destroy(child.gameObject);
		}
	}

	private void CreateOutputs(){
		var layout = outputsPanel.GetComponent<VerticalLayoutGroup>();
		foreach(var output in circuit!.GetOutputs()){
			var originalText = $"{output.Port.OriginalId} = {output.Expression}";
			var colorizedText = ColorizeSpecialChars(originalText);
			var go = Builder.BlueText(colorizedText, layout.transform);
		}
	}

	private string ColorizeSpecialChars(string input){
		const string pattern = @"([\(\)&\|\+\!])";
		var result = Regex.Replace(input, pattern, 
			match => "<color=#" + ColorUtility.ToHtmlStringRGBA(specialCharsColor) + ">" + 
			         match.Value + "</color>");
		return result;
	}
	
	public override bool IsValidForTarget(GameObject target){
		return target.GetComponent<Circuit>() != null;
	}

	public override string GetTitle() => "Circuit properties";
}
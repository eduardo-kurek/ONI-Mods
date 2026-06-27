using CircuitNotIncluded.Core;
using CircuitNotIncluded.Core.DTO;
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
		BuildEditButton();
		builded = true;
	}

	private void BuildContentContainer(){
		ContentContainer = new GameObject("Content");
		ContentContainer.SetParent(gameObject);
		var layout = ContentContainer.AddComponent<VerticalLayoutGroup>();
		layout.spacing = 5;
		layout.padding = new RectOffset(10, 10, 10, 10);
		layout.childAlignment = TextAnchor.MiddleCenter;
		
		var rt = ContentContainer.GetComponent<RectTransform>();
		rt.offsetMax = new Vector2(10, 10);
		rt.offsetMin = new Vector2(10, 10);
	}

	private void BuildCircuitName(){
		var layout = ContentContainer.GetComponent<VerticalLayoutGroup>().transform;
		var go = Utils.UI.PinkText("", layout);
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
		Utils.UI.DarkText("Outputs", layout);
	}

	private void BuildEditButton(){
		PButton editButton = new() {
			Text = "Edit",
			OnClick = OnEditButtonClicked
		};
		editButton.AddTo(ContentContainer);
	}

	private void OnEditButtonClicked(GameObject source){
		CircuitScreenManager.Instance.Build(circuit!);
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
		circuitName.text = circuit!.dto.Name;
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
		foreach(OutputPortDTO o in circuit!.dto.OutputPorts){
			var originalText = $"{o.Bit1.Label} = {o.Bit1.Expression}";
			var colorizedText = Utils.UI.ColorizeExpression(originalText);
			var go = Utils.UI.DarkText(colorizedText, layout.transform);
			var text = go.GetComponent<TextMeshProUGUI>();
			text.color = new Color(0.15f, 0.15f, 0.15f);
		}
	}
	
	public override bool IsValidForTarget(GameObject target){
		return target.GetComponent<Circuit>() != null;
	}

	public override string GetTitle() => "Circuit properties";
}
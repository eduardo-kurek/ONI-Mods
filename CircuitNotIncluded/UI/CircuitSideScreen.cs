using CircuitNotIncluded.Structs;
using PeterHan.PLib.UI;
using UnityEngine;

namespace CircuitNotIncluded.UI;

public class CircuitSideScreen : SideScreenContent {
	private Circuit? circuit = null;
	
	private readonly PButton nameButton = new PButton();
	private readonly PPanel rootPanel = new PPanel() {
		Margin = new RectOffset(10, 10, 10, 10),
	};
	
	protected override void OnPrefabInit(){
		base.OnPrefabInit();
		rootPanel.AddChild(nameButton);
		Refresh();
	}

	public override void SetTarget(GameObject target){
		circuit = target.GetComponent<Circuit>();
		Refresh();
	}
	
	private void Refresh(){
		if(circuit == null) return;
		Destroy(ContentContainer);
		UpdateData();
		ContentContainer = rootPanel.AddTo(gameObject, 0);
	}

	private void UpdateData(){
		nameButton.Text = circuit!.GetCNIName();
	}

	public override bool IsValidForTarget(GameObject target){
		return target.GetComponent<Circuit>() != null;
	}

	public override string GetTitle() => "Circuit properties";
}
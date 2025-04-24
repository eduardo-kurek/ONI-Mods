using UnityEngine;
using UnityEngine.UI;

namespace CircuitNotIncluded.UI.Cells;

public abstract class CircuitCellType {
	protected CircuitCell parent;
	public void SetParent(CircuitCell parent){
		this.parent = parent;
	}
	public abstract GameObject BuildEditorContent();
	protected GameObject BuildContainer(){
		var panel = new GameObject("Panel");
		var layout = panel.AddComponent<VerticalLayoutGroup>();
		layout.childAlignment = TextAnchor.UpperLeft;
		layout.childForceExpandHeight = false;
		layout.childForceExpandWidth = false;
		layout.spacing = 5;
		panel.AddOrGet<LayoutElement>().flexibleHeight = 1;
		panel.GetComponent<LayoutElement>().flexibleWidth = 1;
		return panel;
	}
}
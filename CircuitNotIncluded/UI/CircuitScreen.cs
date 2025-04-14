using CircuitNotIncluded.Structs;
using PeterHan.PLib.Core;
using UnityEngine;
using UnityEngine.UI;

namespace CircuitNotIncluded.UI;

public class CircuitScreen : KModalScreen {
	private static GameObject? parent;
	
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
	
	protected override void OnActivate(){
		base.OnActivate();
		Debug.Log("CircuitScreen activated");
	}

	protected override void OnDeactivate(){
		base.OnDeactivate();
		Debug.Log("CircuitScreen deactivated");
	}
}
using System.Reflection;
using System.Reflection.Emit;
using CircuitNotIncluded.Structs;
using HarmonyLib;
using UnityEngine;

namespace CircuitNotIncluded.Patches;

[HarmonyPatch(typeof(SelectToolHoverTextCard), nameof(SelectToolHoverTextCard.UpdateHoverElements))]
public static class AddCircuitPortHoverCard {
	
	static readonly MethodInfo targetMethod = AccessTools.Method(typeof(HoverTextScreen), nameof(HoverTextScreen.BeginDrawing));
	
	public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions){
		var codes = new List<CodeInstruction>(instructions);

		foreach(CodeInstruction code in codes){
			yield return code;

			if(!BeginDrawingCalled(code)) continue;
			
			yield return new CodeInstruction(OpCodes.Ldarg_0); // this
			yield return new CodeInstruction(OpCodes.Ldarg_1); // hoverObjects
			yield return new CodeInstruction(OpCodes.Call, 
				AccessTools.Method(typeof(AddCircuitPortHoverCard), nameof(DrawCircuitPorts)));
		}
	}

	public static bool BeginDrawingCalled(CodeInstruction code){
		return code.opcode == OpCodes.Callvirt && code.operand is MethodInfo mi && mi == targetMethod;
	}
	
	public static void DrawCircuitPorts(SelectToolHoverTextCard __instance, List<KSelectable> hoverObjects){
		var mode = SimDebugView.Instance.GetMode();
		if (mode != OverlayModes.Logic.ID) return;
		
		int cell = Grid.PosToCell(Camera.main!.ScreenToWorldPoint(KInputManager.GetMousePos()));
		HoverTextDrawer drawer = HoverTextScreen.Instance.drawer;
		
		foreach(var hoverObject in hoverObjects){
			Circuit circuit = hoverObject.GetComponent<Circuit>();
			if (circuit == null) continue;
			circuit.OnHover(cell, drawer, __instance);
		}
	}
	
}
using CircuitNotIncluded.UI;
using HarmonyLib;
using PeterHan.PLib.UI;
using UnityEngine;

namespace CircuitNotIncluded.Patches;

[HarmonyPatch(typeof(DetailsScreen), "OnPrefabInit")]
public class AddSideScreen {
	public static void Postfix(){
		PUIUtils.AddSideScreenContent<CircuitSideScreen>();
	}
}
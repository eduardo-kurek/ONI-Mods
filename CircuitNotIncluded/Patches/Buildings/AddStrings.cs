using HarmonyLib;

namespace CircuitNotIncluded.Patches.Buildings;

[HarmonyPatch(typeof(Localization), "Initialize")]
public class AddStrings {
	public static void Postfix(){
		LocString.CreateLocStringKeys(typeof(STRINGS), null);
	}
}
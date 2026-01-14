using CircuitNotIncluded.Structs;
using HarmonyLib;

namespace CircuitNotIncluded.Patches;

[HarmonyPatch(typeof(GeneratedBuildings), "LoadGeneratedBuildings")]
public class RegisterBuilding {
	public static void Postfix(){
		CircuitManager.Instance.RegisterCircuit(1, 3);
	}
}
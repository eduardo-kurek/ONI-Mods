using CircuitNotIncluded.Structs;
using HarmonyLib;

namespace CircuitNotIncluded.Patches;

[HarmonyPatch(typeof(GeneratedBuildings), "LoadGeneratedBuildings")]
public class RegisterBuilding {
	public static void Postfix(){
		CircuitManager.Instance.RegisterCircuit("logic_new_and_gate", 2, 2, "logic_new_and_kanim");
		CircuitManager.Instance.RegisterCircuit("logic_gate_1x3", 1, 3, "logic_gate_1x3_kanim");
	}
}
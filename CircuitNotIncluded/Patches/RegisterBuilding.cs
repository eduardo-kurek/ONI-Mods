using CircuitNotIncluded.Structs;
using HarmonyLib;

namespace CircuitNotIncluded.Patches;

[HarmonyPatch(typeof(GeneratedBuildings), "LoadGeneratedBuildings")]
public class RegisterBuilding {
	public static void Postfix(){
		CircuitManager.Instance.RegisterCircuit(1, 3);
		CircuitManager.Instance.RegisterCircuit(1, 4);
		CircuitManager.Instance.RegisterCircuit(1, 5);
		
		CircuitManager.Instance.RegisterCircuit(2, 2);
		CircuitManager.Instance.RegisterCircuit(2, 3);
		CircuitManager.Instance.RegisterCircuit(2, 4);
		CircuitManager.Instance.RegisterCircuit(2, 5);
		
		CircuitManager.Instance.RegisterCircuit(3, 3);
		CircuitManager.Instance.RegisterCircuit(3, 4);
		CircuitManager.Instance.RegisterCircuit(3, 5);
		
		CircuitManager.Instance.RegisterCircuit(4, 4);
		CircuitManager.Instance.RegisterCircuit(4, 5);
		
		CircuitManager.Instance.RegisterCircuit(5, 5);
	}
}
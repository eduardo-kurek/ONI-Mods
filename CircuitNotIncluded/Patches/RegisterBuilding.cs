using HarmonyLib;

namespace CircuitNotIncluded.Patches;

[HarmonyPatch(typeof(GeneratedBuildings), "LoadGeneratedBuildings")]
public class RegisterBuilding {
	public static void Postfix(){
		CircuitConfigManager.Instance.RegisterCircuit(1, 3);
		CircuitConfigManager.Instance.RegisterCircuit(1, 4);
		CircuitConfigManager.Instance.RegisterCircuit(1, 5);
		
		CircuitConfigManager.Instance.RegisterCircuit(2, 2);
		CircuitConfigManager.Instance.RegisterCircuit(2, 3);
		CircuitConfigManager.Instance.RegisterCircuit(2, 4);
		CircuitConfigManager.Instance.RegisterCircuit(2, 5);
		
		CircuitConfigManager.Instance.RegisterCircuit(3, 3);
		CircuitConfigManager.Instance.RegisterCircuit(3, 4);
		CircuitConfigManager.Instance.RegisterCircuit(3, 5);
		
		CircuitConfigManager.Instance.RegisterCircuit(4, 4);
		CircuitConfigManager.Instance.RegisterCircuit(4, 5);
		
		CircuitConfigManager.Instance.RegisterCircuit(5, 5);
	}
}
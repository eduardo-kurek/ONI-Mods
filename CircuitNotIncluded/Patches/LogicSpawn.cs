using HarmonyLib;

namespace CircuitNotIncluded.Patches;

[HarmonyPatch(typeof(LogicPorts), "OnSpawn")]
public class LogicSpawn {

	public static void Prefix(LogicPorts __instance){
		BuildingDef def = __instance.GetComponent<Building>().Def;
		Debug.Log("LogicPorts.OnSpawn called from " + def.PrefabID);
	}
	
}

[HarmonyPatch(typeof(LogicPorts), "CreatePhysicalPorts")]
public class PhysycalPorts {

	public static void Prefix(LogicPorts __instance){
		BuildingDef def = __instance.GetComponent<Building>().Def;
		Debug.Log("LogicPorts.CreatePhysicalPorts called from " + def.PrefabID);
	}
	
}
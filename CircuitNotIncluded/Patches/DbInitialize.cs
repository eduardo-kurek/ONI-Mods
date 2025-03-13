using HarmonyLib;

namespace CircuitNotIncluded.Patches;

[HarmonyPatch(typeof(Db), "Initialize")]
public class DbInitialize {
	
	public static void Prefix(){
		Debug.Log("I execute before Db.Initialize!");
	}

	public static void Postfix(){
		Debug.Log("I execute after Db.Initialize!");
	}
	
}
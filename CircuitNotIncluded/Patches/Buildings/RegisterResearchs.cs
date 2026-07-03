using Database;
using HarmonyLib;

namespace CircuitNotIncluded.Patches.Buildings;

[HarmonyPatch(typeof(Techs), "Init")]
public class RegisterResearchs {
	
	public static void Postfix(){
		var tech = Db.Get().Techs.TryGet("DupeTrafficControl"); // Computing research :)
		tech.AddUnlockedItemIDs("Circuit_1x3");
		tech.AddUnlockedItemIDs("Circuit_1x4");
		tech.AddUnlockedItemIDs("Circuit_1x5");
		tech.AddUnlockedItemIDs("Circuit_2x2");
		tech.AddUnlockedItemIDs("Circuit_2x3");
		tech.AddUnlockedItemIDs("Circuit_2x4");
		tech.AddUnlockedItemIDs("Circuit_2x5");
	}
	
}
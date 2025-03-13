using HarmonyLib;
using PeterHan.PLib.Core;

namespace CircuitNotIncluded;

public class CircuitNotIncluded : KMod.UserMod2 {
	
	public override void OnLoad(Harmony harmony){
		base.OnLoad(harmony);
		PUtil.InitLibrary();
	}
	
}
using HarmonyLib;
using PeterHan.PLib.Core;
using PeterHan.PLib.Options;
using Newtonsoft.Json;

namespace CircuitNotIncluded;

public class CircuitNotIncluded : KMod.UserMod2 {
	
	public override void OnLoad(Harmony harmony){
		base.OnLoad(harmony);
		PUtil.InitLibrary();
	}
	
}
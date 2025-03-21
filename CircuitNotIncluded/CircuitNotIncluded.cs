using HarmonyLib;
using Klei.AI;
using PeterHan.PLib.Buildings;
using PeterHan.PLib.Core;
using PeterHan.PLib.Options;
using UnityEngine;

namespace CircuitNotIncluded;

public class CircuitNotIncluded : KMod.UserMod2 {
	
	public override void OnLoad(Harmony harmony){
		base.OnLoad(harmony);
		PUtil.InitLibrary();
		// new POptions().RegisterOptions(this, typeof(ModOptions));
	}
	
}
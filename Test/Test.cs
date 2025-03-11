using HarmonyLib;
using PeterHan.PLib.Core;

namespace Test;

public class Test : KMod.UserMod2 {
	
	public override void OnLoad(Harmony harmony){
		base.OnLoad(harmony);
		PUtil.InitLibrary();
	}
	
}
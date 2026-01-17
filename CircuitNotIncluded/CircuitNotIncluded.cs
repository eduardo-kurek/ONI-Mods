using HarmonyLib;
using PeterHan.PLib.Core;
using PeterHan.PLib.Options;
using Newtonsoft.Json;

namespace CircuitNotIncluded;

[JsonObject(MemberSerialization.OptIn)]
[ModInfo("https://github.com/eduardo-kurek/ONI-Mods")]
public class ModOptions {

	[Option("Cost", "Cost of the Circuit Not Included building")]
	[Limit(1, 1000)]
	[JsonProperty]
	public int Cost { get; set; }

	public ModOptions(){
		Cost = 500;
	}

}

public class CircuitNotIncluded : KMod.UserMod2 {
	
	public override void OnLoad(Harmony harmony){
		base.OnLoad(harmony);
		PUtil.InitLibrary();
		// new POptions().RegisterOptions(this, typeof(ModOptions));
	}
	
}
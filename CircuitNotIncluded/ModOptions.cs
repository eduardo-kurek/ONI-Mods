using Newtonsoft.Json;
using PeterHan.PLib.Options;

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
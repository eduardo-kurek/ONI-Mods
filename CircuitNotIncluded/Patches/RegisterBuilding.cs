using CircuitNotIncluded.Structs;
using HarmonyLib;

namespace CircuitNotIncluded.Patches;

[HarmonyPatch(typeof(GeneratedBuildings), "LoadGeneratedBuildings")]
public class RegisterBuilding {

	public static void Postfix(){
		var i1 = CNIPort.InputPort("i1", new CellOffset(0, 0),
			"i1", "i1", "i1");
		var i2 = CNIPort.InputPort("i2", new CellOffset(0, 1),
			"i2", "i2", "i2");
		var i3 = CNIPort.InputPort("i3", new CellOffset(0, 2),
			"i3", "i3", "i3");
		var o1 = CNIPort.OutputPort("o1", new CellOffset(1, 1),
			"o1", "o1", "o1");

		Output output1 = new Output("i1 & i2 | i3", o1);
			
		CircuitDef newAnd = CircuitDef.Create(
			"New and Gate!","Steins; Gate", "HOUOUYIN KYOUMA-DA",
			2, 3, "logic_new_and_kanim",
			[i1, i2, i3],
			[output1]
		);
		
		
		CircuitManager.Instance.RegisterCircuit(newAnd);
	}
	
}
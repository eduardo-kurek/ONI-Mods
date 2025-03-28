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
		var i3 = CNIPort.InputPort("i3", new CellOffset(1, 0),
			"i3", "i3", "i3");
		var o1 = CNIPort.OutputPort("o1", new CellOffset(1, 1),
			"o1", "o1", "o1");

		Output output1 = new Output("i1 & i2 | i3", o1);
			
		CircuitDef newAnd = CircuitDef.Create(
			"New and Gate!","Steins; Gate", "HOUOUYIN KYOUMA-DA",
			2, 2, "logic_new_and_kanim",
			[i1, i2, i3],
			[output1]
		);
		
		var ii1 = CNIPort.InputPort("i1", new CellOffset(0, 1),
			"i1", "i1", "i1");
		var ii2 = CNIPort.InputPort("i2", new CellOffset(0, 0),
			"i2", "i2", "i2");
		var oo1 = CNIPort.OutputPort("o1", new CellOffset(1, 1),
			"and", "o1", "o1");
		var oo2 = CNIPort.OutputPort("o2", new CellOffset(1, 0),
			"or", "o2", "o2");
		
		Output outputt1 = new Output("i1 & i2", oo1);
		Output outputt2 = new Output("i1 | i2", oo2);
		
		CircuitDef andOr = CircuitDef.Create(
			"And Or","asdf", "asdf",
			2, 2, "logic_new_and_kanim",
			[ii1, ii2],
			[outputt1, outputt2]
		);
		
		CircuitManager.Instance.RegisterCircuit(newAnd);
		CircuitManager.Instance.RegisterCircuit(andOr);
	}
	
}
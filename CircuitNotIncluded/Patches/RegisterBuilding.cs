using CircuitNotIncluded.Structs;
using CircuitNotIncluded.Syntax;
using CircuitNotIncluded.Syntax.Binary;
using CircuitNotIncluded.Syntax.Kinds;
using CircuitNotIncluded.Syntax.Nodes;
using HarmonyLib;
using static LogicPorts;

namespace CircuitNotIncluded.Patches;

[HarmonyPatch(typeof(GeneratedBuildings), "LoadGeneratedBuildings")]
public class RegisterBuilding {

	public static void Postfix(){

		var i1 = Port.InputPort("i1", new CellOffset(0, 2),
			"i1", "i1", "i1");

		var i2 = Port.InputPort("i2", new CellOffset(0, 1),
			"i2", "i2", "i2");

		var i3 = Port.InputPort("i3", new CellOffset(0, 0),
			"i3", "i3", "i3");

		var o1 = Port.OutputPort("o1", new CellOffset(1, 1),
			"o1", "o1", "o1");
		
		
		Identifier _i1 = new Identifier(i1);
		Identifier _i2 = new Identifier(i2);
		Identifier _i3 = new Identifier(i3);
		
		BinaryOperation and = new BinaryOperation(_i1, _i2, new AndKind());
		
		BinaryOperation or = new BinaryOperation(and, _i3, new OrKind());
		
		SyntaxTree o1Syntax = new SyntaxTree(o1, or);
		 
		CircuitDef circuit = CircuitDef.Create(
			"New and Gate!","Steins; Gate", "HOUOUYIN KYOUMA-DA",
			2, 3, "logic_new_and_kanim",
			new List<Port> { i1, i2, i3 },
			new List<SyntaxTree> { o1Syntax }
		);
		
		Debug.Log("I1: " + i1.id);
		Debug.Log("I2: " + i2.id);
		Debug.Log("I3: " + i3.id);
		Debug.Log("O1: " + o1.id);
		
		CircuitManager.Instance.RegisterCircuit(circuit);
	}
	
}
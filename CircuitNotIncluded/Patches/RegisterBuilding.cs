using CircuitNotIncluded.Structs;
using CircuitNotIncluded.Syntax;
using CircuitNotIncluded.Syntax.Kinds;
using CircuitNotIncluded.Syntax.Nodes;
using HarmonyLib;
using static LogicPorts;

namespace CircuitNotIncluded.Patches;

[HarmonyPatch(typeof(GeneratedBuildings), "LoadGeneratedBuildings")]
public class RegisterBuilding {

	public static void Postfix(){

		var i1 = Port.InputPort("i1", new CellOffset(0, 0),
			"i1", "i1", "i1");

		var i2 = Port.InputPort("i2", new CellOffset(0, 1),
			"i2", "i2", "i2");

		var i3 = Port.InputPort("i3", new CellOffset(1, 0),
			"i3", "i3", "i3");

		var o1 = Port.OutputPort("o1", new CellOffset(1, 1),
			"o1", "o1", "o1");
		
		
		Identifier _i1 = new Identifier(i1);
		Identifier _i2 = new Identifier(i2);
		Identifier _i3 = new Identifier(i3);
		
		BinaryOperation and = new BinaryOperation(_i1, _i2, AndKind.Instance);
		BinaryOperation or = new BinaryOperation(and, _i3, OrKind.Instance);
		
		SyntaxTree o1Syntax = new SyntaxTree(o1, or);
		 
		CircuitDef newAnd = CircuitDef.Create(
			"New and Gate!","Steins; Gate", "HOUOUYIN KYOUMA-DA",
			2, 2, "logic_new_and_kanim",
			new List<Port> { i1, i2, i3 },
			new List<SyntaxTree> { o1Syntax }
		);
		
		var ii1 = Port.InputPort("i1", new CellOffset(0, 1),
			"i1", "i1", "i1");

		var ii2 = Port.InputPort("i2", new CellOffset(0, 0),
			"i2", "i2", "i2");

		var oo1 = Port.OutputPort("o1", new CellOffset(1, 1),
			"and", "o1", "o1");
		
		var oo2 = Port.OutputPort("o2", new CellOffset(1, 0),
			"or", "o2", "o2");

		Identifier __i1 = new Identifier(ii1);
		Identifier __i2 = new Identifier(ii2);

		BinaryOperation and2 = new BinaryOperation(__i1, __i2, AndKind.Instance);
		BinaryOperation or2 = new BinaryOperation(__i1, __i2, OrKind.Instance);
		
		SyntaxTree oo1Syntax = new SyntaxTree(oo1, and2);
		SyntaxTree oo2Syntax = new SyntaxTree(oo2, or2);
		
		CircuitDef andOr = CircuitDef.Create(
			"And Or","desc", "asdf",
			2, 2, "logic_new_and_kanim",
			new List<Port> { ii1, ii2, },
			new List<SyntaxTree> { oo1Syntax, oo2Syntax }
		);
		
		CircuitManager.Instance.RegisterCircuit(newAnd);
		CircuitManager.Instance.RegisterCircuit(andOr);
	}
	
}
using CircuitNotIncluded.Syntax;
using CircuitNotIncluded.Syntax.Visitors;

namespace CircuitNotIncluded.Structs;

public class Circuit : KMonoBehaviour {

	private LogicPorts ports = null!;
	private CircuitDef circuitDef = null!;
	private DependencyTable dependencyTable = null!;
	private LogicValueChanged lastChange;
	
	protected override void OnSpawn(){
		ports = GetComponent<LogicPorts>();
		circuitDef = (CircuitDef)GetComponent<Building>().Def;
		dependencyTable = new DependencyTable(circuitDef);
		
		Subscribe((int)GameHashes.LogicEvent, data => {
			lastChange = (LogicValueChanged)data;
			OnNetworkValueChanged();
		});
		
		ApplyChanges();
	}

	private void OnNetworkValueChanged(){
		if(IsInputPort() && ValueChanged())
			OnInputPortChanged();
	}

	private bool IsInputPort(){
		return dependencyTable.HasInputPort(lastChange.portID);
	}

	private bool ValueChanged(){
		return lastChange.prevValue != lastChange.newValue;
	}

	private void OnInputPortChanged(){
		var outDependents = dependencyTable.GetDependents(lastChange.portID);
		foreach(SyntaxTree tree in outDependents){
			SyntaxEvaluater evaluater = new SyntaxEvaluater(ports);
			tree.Accept(evaluater);
			int result = evaluater.GetResult();
			ports.SendSignal(tree.OutputPort.P.id, result);
		}
	}

	public void ApplyChanges(){
		ports.inputPortInfo = circuitDef.CNI_InputPorts.Select(port => port.P).ToArray();
		ports.outputPortInfo = circuitDef.CNI_Outputs.Select(tree => tree.OutputPort.P).ToArray();
		RefreshPhysicalPorts();
	}

	// When you call SendSignal and the outputPorts is null, the game will call _ports.CreatePhysicalPorts
	public void RefreshPhysicalPorts(){
		ports.outputPorts = null;
		ports.SendSignal("", 0);
	}
	
	public string GetCNIName() => circuitDef.CNI_Name;
	
}
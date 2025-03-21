using CircuitNotIncluded.Syntax;
using CircuitNotIncluded.Syntax.Visitors;

namespace CircuitNotIncluded.Structs;

internal class Circuit : KMonoBehaviour {

	private LogicPorts _ports = null!;
	private CircuitDef _circuitDef = null!;
	private DependencyTable _dependencyTable = null!;
	private LogicValueChanged _lastChange = new LogicValueChanged();
	

	protected override void OnSpawn(){
		_ports = GetComponent<LogicPorts>();
		_circuitDef = (CircuitDef)GetComponent<Building>().Def;
		_dependencyTable = new DependencyTable(_circuitDef);
		
		Subscribe((int)GameHashes.LogicEvent, data => {
			_lastChange = (LogicValueChanged)data;
			OnNetworkValueChanged();
		});
		
		ApplyChanges();
	}

	private void OnNetworkValueChanged(){
		if(IsInputPort() && ValueChanged())
			OnInputPortChanged();
	}

	private bool IsInputPort(){
		return _dependencyTable.HasInputPort(_lastChange.portID);
	}

	private bool ValueChanged(){
		return _lastChange.prevValue != _lastChange.newValue;
	}

	private void OnInputPortChanged(){
		var outDependents = _dependencyTable.GetDependents(_lastChange.portID);
		foreach(SyntaxTree tree in outDependents){
			SyntaxEvaluater evaluater = new SyntaxEvaluater(_ports);
			tree.Accept(evaluater);
			int result = evaluater.GetResult();
			_ports.SendSignal(tree.OutputPort.id, result);
		}
	}

	public void ApplyChanges(){
		_ports.inputPortInfo = _circuitDef.CNI_InputPorts.ToArray();
		_ports.outputPortInfo = _circuitDef.CNI_Outputs.Select(tree => tree.OutputPort).ToArray();
		RefreshPhysicalPorts();
	}

	// When you call SendSignal and the outputPorts is null, the game will call _ports.CreatePhysicalPorts
	public void RefreshPhysicalPorts(){
		_ports.outputPorts = null;
		_ports.SendSignal("", 0);
	}
	
}
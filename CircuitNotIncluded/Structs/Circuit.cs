using System.Runtime.CompilerServices;

namespace CircuitNotIncluded.Structs;

public class Circuit : KMonoBehaviour {
	private LogicPorts ports = null!;
	private CircuitDef circuitDef = null!;
	private DependencyTable dependencyTable = null!;
	private SymbolTable symbolTable = null!;
	private LogicValueChanged lastChange;
	
	protected override void OnSpawn(){
		ports = GetComponent<LogicPorts>();
		circuitDef = (CircuitDef)GetComponent<Building>().Def;
		dependencyTable = new DependencyTable(circuitDef);
		symbolTable = new SymbolTable(ports, circuitDef);
		
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
		var inputId = GetInputPortId();
		var outDependents = dependencyTable.GetOutputDependents(inputId);
		foreach(Output output in outDependents)
			output.Update(symbolTable);
	}
	
	private HashedString GetInputPortId() => lastChange.portID;

	public void ApplyChanges(){
		ports.inputPortInfo = circuitDef.CNI_InputPorts.Select(port => port.P).ToArray();
		ports.outputPortInfo = circuitDef.CNI_Outputs.Select(output => output.Port.P).ToArray();
		RefreshPhysicalPorts();
		UpdateAllOutputs();
	}

	// When you call SendSignal and the outputPorts is null, the game will call ports.CreatePhysicalPorts
	private void RefreshPhysicalPorts(){
		ports.outputPorts = null;
		ports.SendSignal("", 0);
	}
	
	private void UpdateAllOutputs(){
		foreach(Output output in circuitDef.CNI_Outputs)
			output.Update(symbolTable);
	}
	
	public string GetCNIName() => circuitDef.CNI_Name;
	public List<Output> GetOutputs() => circuitDef.CNI_Outputs;
}
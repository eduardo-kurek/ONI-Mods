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
	public int GetWidth() => circuitDef.WidthInCells;
	public int GetHeight() => circuitDef.HeightInCells;

	public void Print(){
		Debug.Log($"Name: {circuitDef.CNI_Name}");
		Debug.Log($"Width: {circuitDef.WidthInCells}");
		Debug.Log($"Height: {circuitDef.HeightInCells}");
		Debug.Log("Input ports:");
		foreach(var i in circuitDef.CNI_InputPorts){
			Debug.Log("Name: " + i.OriginalId);
			Debug.Log("X: " + i.P.cellOffset.x);
			Debug.Log("Y: " + i.P.cellOffset.y);
		}
		
		Debug.Log("Output ports:");
		foreach(var o in circuitDef.CNI_Outputs){
			Debug.Log("Name: " + o.Port.OriginalId);
			Debug.Log("X: " + o.Port.P.cellOffset.x);
			Debug.Log("Y: " + o.Port.P.cellOffset.y);
		}
	}
	public List<Output> GetOutputs() => circuitDef.CNI_Outputs;
}
using System.Runtime.CompilerServices;
using CircuitNotIncluded.UI.Cells;
using TemplateClasses;

namespace CircuitNotIncluded.Structs;

public class Circuit : KMonoBehaviour {
	private LogicPorts logicPorts = null!;
	private BuildingDef def = null!;
	private DependencyTable dependencyTable = null!;
	private SymbolTable symbolTable = null!;
	private List<CNIPort> inputPorts;
	private List<Output> outputPorts;
	private LogicValueChanged lastChange;

	protected override void OnSpawn(){
		InitMembers();
		UpdatePorts([], []);
		ApplyChanges();
		SubscribeNetworkEvent();
	}

	private void InitMembers(){
		logicPorts = GetComponent<LogicPorts>();
		def = GetComponent<Building>().Def;
	}

	private void UpdatePorts(List<CNIPort> inputPorts, List<Output> outputPorts){
		this.inputPorts = inputPorts;
		this.outputPorts = outputPorts;
		dependencyTable = new DependencyTable(inputPorts, outputPorts);
		symbolTable = new SymbolTable(logicPorts, inputPorts);
	}

	private void SubscribeNetworkEvent(){
		Subscribe((int)GameHashes.LogicEvent, data => {
			lastChange = (LogicValueChanged)data;
			OnNetworkValueChanged();
		});
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

	public void Refresh(List<CNIPort> inputs, List<Output> outputs){
		UpdatePorts(inputs, outputs);
		ApplyChanges();
	}
	
	private void ApplyChanges(){
		logicPorts.inputPortInfo = inputPorts.Select(port => port.P).ToArray();
		logicPorts.outputPortInfo = outputPorts.Select(output => output.Port.P).ToArray();
		RefreshPhysicalPorts();
		UpdateAllOutputsSignal();
	}

	// When you call SendSignal and the outputPorts is null, the game will call ports.CreatePhysicalPorts
	private void RefreshPhysicalPorts(){
		logicPorts.outputPorts = null;
		logicPorts.SendSignal("", 0);
	}

	private void UpdateAllOutputsSignal(){
		foreach(Output output in outputPorts)
			output.Update(symbolTable);
	}

	public string GetCNIName() => def.PrefabID;
	public int GetWidth() => def.WidthInCells;
	public int GetHeight() => def.HeightInCells;
	public List<CNIPort> GetInputPorts() => inputPorts;
	public List<Output> GetOutputs() => outputPorts;

	// Converts a 2D CellOffset to a linear offset.
	// The index starts on the left-bottom, and goes to the right-up.
	public int ToLinearIndex(CellOffset offset){
		int width = GetWidth();
		return width * offset.y + offset.x;
	}

	public int GetActualCell(CellOffset offset){
		var component = base.GetComponent<Rotatable>();
		if(component != null)
			offset = component.GetRotatedCellOffset(offset);
		return Grid.OffsetCell(Grid.PosToCell(transform.GetPosition()), offset);
	}

	// Converts a linear index to a 2D CellOffset.
	// The index starts on the left-bottom, and goes to the right-up.
	public CellOffset ToCellOffset(int index){
		int width = GetWidth();
		return new CellOffset(index % width, index / width);
	}

	public void Print(){
		Debug.Log($"Id: {def.PrefabID}");
		Debug.Log($"Width: {def.WidthInCells}");
		Debug.Log($"Height: {def.HeightInCells}");
		Debug.Log("Input ports:");
		foreach(CNIPort i in inputPorts){
			Debug.Log("Name: " + i.OriginalId);
			Debug.Log("X: " + i.P.cellOffset.x);
			Debug.Log("Y: " + i.P.cellOffset.y);
		}

		Debug.Log("Output ports:");
		foreach(Output o in outputPorts){
			Debug.Log("Name: " + o.Port.OriginalId);
			Debug.Log("X: " + o.Port.P.cellOffset.x);
			Debug.Log("Y: " + o.Port.P.cellOffset.y);
		}
	}
}
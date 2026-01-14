using System.Runtime.CompilerServices;
using CircuitNotIncluded.UI.Cells;
using TemplateClasses;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace CircuitNotIncluded.Structs;

public class Circuit : KMonoBehaviour {
	private const int CELL_SIZE_PX = 100;
	
	private LogicPorts logicPorts = null!;
	private BuildingDef def = null!;
	private DependencyTable dependencyTable = null!;
	private SymbolTable symbolTable = null!;
	
	public List<CNIPort> InputPorts { get; private set; } = null!;
	public List<Output> OutputPorts { get; private set; } = null!;
	private LogicValueChanged lastChange = null!;
	
	public string CNIName => def.PrefabID;
	public int Width => def.WidthInCells;
	public int Height => def.HeightInCells;
	public List<Output> GetOutputs() => OutputPorts;

	protected override void OnSpawn(){
		InitMembers();
		SetPorts([], []);
		SubscribeNetworkEvent();
	}

	private void InitMembers(){
		logicPorts = GetComponent<LogicPorts>();
		def = GetComponent<Building>().Def;
	}

	public void SetPorts(List<CNIPort> inputPorts, List<Output> outputPorts){
		InternalSetPorts(inputPorts, outputPorts);
		RefreshCircuit();
	}
	
	private void InternalSetPorts(List<CNIPort> inputPorts, List<Output> outputPorts){
		InputPorts = inputPorts;
		logicPorts.inputPortInfo = InputPorts.Select(port => port.P).ToArray();
		OutputPorts = outputPorts;
		logicPorts.outputPortInfo = OutputPorts.Select(output => output.Port.P).ToArray();
		dependencyTable = new DependencyTable(inputPorts, outputPorts);
		symbolTable = new SymbolTable(logicPorts, inputPorts);
	}
	
	private void RefreshCircuit(){
		RefreshPhysicalPorts();
		RefreshSignals();
	}

	// When you call SendSignal and the outputPorts is null, the game will call ports.CreatePhysicalPorts
	private void RefreshPhysicalPorts(){
		logicPorts.outputPorts = null;
		logicPorts.SendSignal("", 0);
	}
	
	private void RefreshSignals(){
		foreach(Output output in OutputPorts)
			output.Update(symbolTable);
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

	private bool IsInputPort() => dependencyTable.HasInputPort(lastChange.portID);
	private bool ValueChanged() => lastChange.prevValue != lastChange.newValue;

	private void OnInputPortChanged(){
		HashedString lastChangedPortId = lastChange.portID;
		UpdateAllDependentsOf(lastChangedPortId);
	}

	private void UpdateAllDependentsOf(HashedString portId){
		var outDependents = dependencyTable.GetOutputDependents(portId);
		foreach(Output output in outDependents)
			output.Update(symbolTable);
	}

	public int GetGlobalPositionCell(CellOffset offset){
		var component = GetComponent<Rotatable>();
		if(component != null)
			offset = component.GetRotatedCellOffset(offset);
		return Grid.OffsetCell(Grid.PosToCell(transform.GetPosition()), offset);
	}
	
	public Sprite GetOffSprite(){
		var animFiles = def.AnimFiles;
		Texture2D texture = animFiles.First().textureList.First();
		var rect = new Rect(0,
			texture.height - (Height * CELL_SIZE_PX),
			Width * CELL_SIZE_PX,
			Height * CELL_SIZE_PX
		);
		return Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));
	}
	
	// Converts a 2D CellOffset to a linear offset.
	// The index starts on the left-bottom, and goes to the right-up.
	public int ToLinearIndex(CellOffset offset) => Width * offset.y + offset.x;
	
	// Converts a linear index to a 2D CellOffset.
	// The index starts on the left-bottom, and goes to the right-up.
	public CellOffset ToCellOffset(int index) => new CellOffset(index % Width, index / Width);
}
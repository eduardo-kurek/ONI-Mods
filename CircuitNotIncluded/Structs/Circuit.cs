using System.Runtime.CompilerServices;
using CircuitNotIncluded.UI.Cells;
using TemplateClasses;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace CircuitNotIncluded.Structs;

public class Circuit : KMonoBehaviour {
	private const int SPRITE_TILE_SIZE = 100;
	
	private LogicPorts logicPorts = null!;
	private BuildingDef def = null!;
	private DependencyTable dependencyTable = null!;
	private SymbolTable symbolTable = null!;
	
	public List<InputPort> InputPorts { get; private set; } = null!;
	public List<OutputPort> OutputPorts { get; private set; } = null!;
	
	private LogicValueChanged lastChange = null!;
	
	public string CNIName => def.PrefabID;
	public int Width => def.WidthInCells;
	public int Height => def.HeightInCells;
	public List<OutputPort> GetOutputs() => OutputPorts;

	protected override void OnSpawn(){
		InitMembers();
		SetPorts([], []);
		SubscribeNetworkEvent();
	}

	private void InitMembers(){
		logicPorts = GetComponent<LogicPorts>();
		def = GetComponent<Building>().Def;
	}

	public void SetPorts(List<InputPort> inputPorts, List<OutputPort> outputPorts){
		InternalSetPorts(inputPorts, outputPorts);
		RefreshCircuit();
	}
	
	private void InternalSetPorts(List<InputPort> inputPorts, List<OutputPort> outputPorts){
		InputPorts = inputPorts;
		logicPorts.inputPortInfo = InputPorts.Select(port => port.WrappedPort).ToArray();
		OutputPorts = outputPorts;
		logicPorts.outputPortInfo = OutputPorts.Select(output => output.WrappedPort).ToArray();
		dependencyTable = new DependencyTable(inputPorts, outputPorts);
		symbolTable = new SymbolTable(logicPorts, inputPorts);
	}
	
	private void RefreshCircuit(){
		RefreshPhysicalPorts();
		RefreshSignals(OutputPorts);
	}

	// When you call SendSignal and the outputPorts is null, the game will call ports.CreatePhysicalPorts
	private void RefreshPhysicalPorts(){
		logicPorts.outputPorts = null;
		logicPorts.SendSignal("", 0);
	}
	
	private void RefreshSignals(List<OutputPort> outputs){
		foreach(OutputPort output in outputs){
			int result = output.Evaluate(symbolTable);
			SendSignalToOutput(output, result);
		}
	}

	private void SendSignalToOutput(OutputPort outputPort, int signal){
		logicPorts.SendSignal(outputPort.HashedId, signal);
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
		RefreshAllDependentsOf(lastChangedPortId);
	}

	private void RefreshAllDependentsOf(HashedString portId){
		var outDependents = dependencyTable.GetOutputDependents(portId);
		RefreshSignals(outDependents);
	}

	public int GetGlobalPositionCell(CellOffset offset){
		var component = GetComponent<Rotatable>();
		if(component != null)
			offset = component.GetRotatedCellOffset(offset);
		return Grid.OffsetCell(Grid.PosToCell(transform.GetPosition()), offset);
	}
	
	public Sprite GetCircuitSprite(){
		var animFiles = def.AnimFiles;
		Texture2D texture = animFiles.First().textureList.First();
		var rect = new Rect(0,
			texture.height - (Height * SPRITE_TILE_SIZE),
			Width * SPRITE_TILE_SIZE,
			Height * SPRITE_TILE_SIZE
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
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

	/**
	 * When the circuit has width greater or equal than 3,
	 * its origin is not (0,0) anymore, but turns into negative values. Examples:
	 * 3x3 -> (-1,0); 4x4 -> (-1,0)
	 * 5x5 -> (-2,0); 6x6 -> (-2,0) and so on.
	 * The following formula is to get this offset value
	 */
	private int OriginOffset => (Width - 1) / 2;
	
	// Converts a 2D CellOffset to a linear offset.
	// The index starts on the left-bottom, and goes to the right-up.
	public int ToGridIndex(CellOffset offset) => Width * offset.y + offset.x + OriginOffset;

	// Converts a 2D CellOffset to a display index (Top-Left).
	public int ToDisplayIndex(CellOffset offset) {
		CellOffset mirrored = offset;
		mirrored.y = (Height - 1) - offset.y;
		return ToGridIndex(mirrored);
	}
	
	// Converts a linear index to a 2D CellOffset.
	// The index starts on the left-bottom, and goes to the right-up.
	public CellOffset ToCellOffset(int index) => new(index % Width - OriginOffset, index / Width);
}
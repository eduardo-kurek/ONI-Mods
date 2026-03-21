using CircuitNotIncluded.Grammar;
using KSerialization;
using UnityEngine;
using static EventSystem;

namespace CircuitNotIncluded.Structs;

[SerializationConfig(MemberSerialization.OptIn)]
public class Circuit : KMonoBehaviour {
	private const int SPRITE_TILE_SIZE = 100;
	
	private BuildingDef def = null!;
	public int Width => def.WidthInCells;
	public int Height => def.HeightInCells;
	
	[Serialize] public string CNIName { get; set; } = "Circuit Name";
	[Serialize] public List<CircuitInput> Inputs = [];
	[Serialize] public List<CircuitOutput> Outputs = [];
	
	private readonly DependencyTable dependencyTable = new();
	private readonly SymbolTable symbolTable = new();
	
	private static readonly IntraObjectHandler<Circuit> OnBuildingBrokenDelegate = new (delegate(Circuit component, object data){
		component.Disconnect();
	});
	
	private static readonly IntraObjectHandler<Circuit> OnBuildingFullyRepairedDelegate = new (delegate(Circuit component, object data){
		component.Connect();
	});

	private bool connected;
	private bool cleaningUp;

	protected override void OnSpawn(){
		InitMembers();
		HydratePorts();
		CreatePhysicalPorts();
		SetUpEvents();
	}
	
	private void SetUpEvents(){
		Subscribe((int)GameHashes.BuildingBroken, OnBuildingBrokenDelegate);
		Subscribe((int)GameHashes.BuildingFullyRepaired, OnBuildingFullyRepairedDelegate);
	}

	private void InitMembers(){
		def = GetComponent<Building>().Def;
	}
	
	private void HydratePorts() {
		if (Inputs.Count == 0 && Outputs.Count == 0) return;

		foreach (var input in Inputs) {
			input.parent = this;
		}

		foreach (var output in Outputs) {
			output.parent = this;
			output.symbolTable = symbolTable;
		}
	}
	
	public void CreatePhysicalPorts(){
		RebuildDependencyGraph();
		ConnectIfNotBroken();
	}
	
	private void RebuildDependencyGraph(){
		foreach (CircuitOutput output in Outputs) {
			var usedInputIds = Compiler.ExtractIds(output.port.Tree);

			foreach(string inputId in usedInputIds)
				dependencyTable.RegisterDependency(inputId, output);
		}
	}
	
	private void ConnectIfNotBroken(){
		BuildingHP component = GetComponent<BuildingHP>();
		if (component == null || !component.IsBroken)
			Connect();
	}
	
	private void Connect(){
		if(connected) return;
		foreach(CircuitPort port in Inputs) 
			port.Connect();
		foreach(CircuitPort port in Outputs) 
			port.Connect();
		connected = true;
	}

	public void SetPorts(List<InputPort> inputPorts, List<OutputPort> outputPorts){
		ResetCircuit();
		PreparePorts(inputPorts, outputPorts);
		CreatePhysicalPorts();
	}
	
	private void ResetCircuit(){
		symbolTable.Clear();
		dependencyTable.Clear();
		Disconnect();
		Inputs.Clear();
		Outputs.Clear();
	}
	
	private void Disconnect(){
		if(!connected) return;
		foreach(CircuitPort port in Inputs) 
			port.Disconnect();
		foreach(CircuitPort port in Outputs) 
			port.Disconnect();
		connected = false;
	}

	private void PreparePorts(List<InputPort> inputPorts, List<OutputPort> outputPorts){
		foreach(InputPort inputPort in inputPorts){
			CircuitInput input = new(this, inputPort);
			Inputs.Add(input);
		}

		foreach(OutputPort outputPort in outputPorts){
			CircuitOutput output = new(this, outputPort, symbolTable);
			Outputs.Add(output);
		}
	}
	
	protected override void OnCleanUp(){
		UnityEngine.Debug.Log("Cleaning up circuit");
		cleaningUp = true;
		Disconnect();
		CleanUpEvents();
	}

	private void CleanUpEvents(){
		Unsubscribe((int)GameHashes.BuildingBroken, OnBuildingBrokenDelegate);
		Unsubscribe((int)GameHashes.BuildingFullyRepaired, OnBuildingFullyRepairedDelegate);
	}

	public void OnInputPortChanged(string inputId, int newValue){
		if(cleaningUp) return;
		if (symbolTable.GetValue(inputId) == newValue) return;
		symbolTable.SetValue(inputId, newValue);
		
		var dependents = dependencyTable.GetOutputDependents(inputId);
		foreach(CircuitOutput output in dependents)
			output.Refresh();
	}

	public int GetActualCell(CellOffset offset){
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
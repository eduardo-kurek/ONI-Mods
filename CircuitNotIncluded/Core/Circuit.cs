using CircuitNotIncluded.Core.DTO;
using CircuitNotIncluded.Core.Model;
using CircuitNotIncluded.Interfaces;
using CircuitNotIncluded.Core.Runtime;
using KSerialization;
using UnityEngine;
using static EventSystem;

namespace CircuitNotIncluded.Core;

[SerializationConfig(MemberSerialization.OptIn)]
public sealed partial class Circuit : KMonoBehaviour {
	private const int SPRITE_TILE_SIZE = 100;
	
	private BuildingDef def = null!;
	public int Width => def.WidthInCells;
	public int Height => def.HeightInCells;

	[Serialize] public CircuitDTO dto = CircuitDTO.Empty();
	private CircuitRuntime runtime = null!;
	
	private static readonly IntraObjectHandler<Circuit> OnBuildingBrokenDelegate = new (delegate(Circuit component, object data){
		component.Disconnect();
	});
	
	private static readonly IntraObjectHandler<Circuit> OnBuildingFullyRepairedDelegate = new (delegate(Circuit component, object data){
		component.Connect();
	});
	
	private static readonly IntraObjectHandler<Circuit> OnCopySettingsDelegate = new (delegate(Circuit component, object data){
		component.OnCopySettings(data);
	});

	private bool connected;
	private bool cleaningUp;

	protected override void OnSpawn(){
		InitMembers();
		UpdateRuntime();
		ConnectIfNotBroken();
		SetUpEvents();
	}

	private void InitMembers(){
		def = GetComponent<Building>().Def;
	}

	private void UpdateRuntime(){
		var model = new CircuitModel(dto, GetActualCell);
		runtime = new CircuitRuntime(model);
	}
	
	private void ConnectIfNotBroken(){
		BuildingHP component = GetComponent<BuildingHP>();
		if (component == null || !component.IsBroken)
			Connect();
	}
	
	private void Connect(){
		if(connected) return;
		runtime.Connect();
		connected = true;
	}
	
	private void Disconnect(){
		if(!connected) return;
		runtime.Disconnect();
		connected = false;
	}
	
	private void SetUpEvents(){
		Subscribe((int)GameHashes.CopySettings, OnCopySettingsDelegate);
		Subscribe((int)GameHashes.BuildingBroken, OnBuildingBrokenDelegate);
		Subscribe((int)GameHashes.BuildingFullyRepaired, OnBuildingFullyRepairedDelegate);
	}
	
	private void CleanUpEvents(){
		Unsubscribe((int)GameHashes.CopySettings, OnCopySettingsDelegate);
		Unsubscribe((int)GameHashes.BuildingBroken, OnBuildingBrokenDelegate);
		Unsubscribe((int)GameHashes.BuildingFullyRepaired, OnBuildingFullyRepairedDelegate);
	}

	public void SetData(CircuitDTO circuitDto){
		dto = circuitDto;
		Disconnect();
		UpdateRuntime();
		ConnectIfNotBroken();
	}

	private void OnCopySettings(object data){
		Circuit source = ((GameObject)data).GetComponent<Circuit>();
		if(source == null) return;
		SetData(source.dto with { });
	}
	
	protected override void OnCleanUp(){
		cleaningUp = true;
		Disconnect();
		CleanUpEvents();
	}
	
	public void OnHover(int cell, HoverTextDrawer drawer, SelectToolHoverTextCard cfg){
		var portInCell = TryGetPortAtCell(cell);
		if(portInCell == null) return;
		
		drawer.BeginShadowBar();
		portInCell.OnHover(dto.Name, drawer, cfg);
		drawer.EndShadowBar();
	}
	
	public IHover? TryGetPortAtCell(int cell){
		foreach(InputPortDTO? i in dto.InputPorts.Where(i => GetActualCell(i.Offset) == cell))
			return i;
		foreach(OutputPortDTO? o in dto.OutputPorts.Where(i => GetActualCell(i.Offset) == cell))
			return o;
		return null;
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
	public int ToDisplayIndex(CellOffset offset){
		CellOffset mirrored = offset;
		mirrored.y = (Height - 1) - offset.y;
		return ToGridIndex(mirrored);
	}
	
	// Converts a linear index to a 2D CellOffset.
	// The index starts on the left-bottom, and goes to the right-up.
	public CellOffset ToCellOffset(int index) => new(index % Width - OriginOffset, index / Width);
}
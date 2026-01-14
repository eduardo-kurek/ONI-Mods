using CircuitNotIncluded.Structs;
using CircuitNotIncluded.Utils;
using Klei.AI;
using UnityEngine;

namespace CircuitNotIncluded;

/** 
 * This class is equivalent to `BuildingConfigManager` in the original game,
 * But is exclusive to the mod and is used to manage the circuits.
 */
internal sealed class CircuitManager {
	private GameObject? baseTemplate;
	private static readonly Lazy<CircuitManager> instance = new(() => new CircuitManager());
	public static CircuitManager Instance => instance.Value;

	private CircuitManager(){
		InitializeBaseTemplate();
	}

	private void InitializeBaseTemplate(){
		baseTemplate = new GameObject("CircuitTemplate");
		baseTemplate.SetActive(false);
		baseTemplate.AddComponent<KPrefabID>();
		baseTemplate.AddComponent<KSelectable>();
		baseTemplate.AddComponent<Modifiers>();
		baseTemplate.AddComponent<PrimaryElement>();
		baseTemplate.AddComponent<BuildingComplete>();
		baseTemplate.AddComponent<StateMachineController>();
		baseTemplate.AddComponent<Deconstructable>();
		baseTemplate.AddComponent<Reconstructable>();
		baseTemplate.AddComponent<SaveLoadRoot>();
		baseTemplate.AddComponent<OccupyArea>();
		baseTemplate.AddComponent<DecorProvider>();
		baseTemplate.AddComponent<Operational>();
		baseTemplate.AddComponent<BuildingEnabledButton>();
		baseTemplate.AddComponent<Prioritizable>();
		baseTemplate.AddComponent<BuildingHP>();
		baseTemplate.AddComponent<LoopingSounds>();
		baseTemplate.AddComponent<InvalidPortReporter>();
	}
	
	public void RegisterCircuit(int width, int height){
		string name = $"Circuit {width}x{height}";
		string anim = $"logic_circuit_{width}x{height}_kanim";
		RegisterCircuit(name, width, height, anim);
	}
	
	public void RegisterCircuit(string name, int width, int height, string anim){
		BuildingDef def = CircuitDefFactory.Create(name, width, height, anim);
		GameObject gameObject = UnityEngine.Object.Instantiate(baseTemplate!);
		UnityEngine.Object.DontDestroyOnLoad(gameObject);
		KPrefabID component = gameObject.GetComponent<KPrefabID>();
		component.PrefabTag = def.Tag;
		component.SetDlcRestrictions(def);
		gameObject.name = def.PrefabID + "Template";
		gameObject.GetComponent<Building>().Def = def;
		gameObject.GetComponent<OccupyArea>().SetCellOffsets(def.PlacementOffsets);
		gameObject.AddTag(GameTags.RoomProberBuilding);
		def.BuildingComplete = BuildingLoader.Instance.CreateBuildingComplete(gameObject, def);
		def.BuildingUnderConstruction = BuildingLoader.Instance.CreateBuildingUnderConstruction(def);
		def.BuildingUnderConstruction.name = BuildingConfigManager.GetUnderConstructionName(def.BuildingUnderConstruction.name);
		def.BuildingPreview = BuildingLoader.Instance.CreateBuildingPreview(def);
		GameObject buildingPreview = def.BuildingPreview;
		buildingPreview.name += "Preview";
			
		def.PostProcess();
		
		def.BuildingComplete.AddComponent<LogicPorts>();
		def.BuildingComplete.AddComponent<Circuit>();
		
		Assets.AddBuildingDef(def);
		
		Utilss.AddBuildingStrings(def.PrefabID, name, "Description", "Effect");
		ModUtil.AddBuildingToPlanScreen("Automation", def.PrefabID, "wires", "LogicWire");
		Debug.Log("Circuit registered successfully: " + def.PrefabID);
	}
	
}
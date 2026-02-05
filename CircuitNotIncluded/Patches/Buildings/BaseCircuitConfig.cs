using CircuitNotIncluded.Structs;
using CircuitNotIncluded.Utils;
using STRINGS;
using TUNING;
using UnityEngine;
using BUILDINGS = TUNING.BUILDINGS;

namespace CircuitNotIncluded.Patches.Buildings;

public abstract class BaseCircuitConfig : IBuildingConfig {
	protected abstract int Width { get; }
	protected abstract int Height { get; }
	protected virtual string Name => $"Circuit {Width}x{Height}";
	protected virtual string Anim => $"logic_circuit_{Width}x{Height}_kanim";
	
	public override BuildingDef CreateBuildingDef(){
		var def = BuildingTemplates.CreateBuildingDef(Name, Width, Height, Anim, 10,
			3f, BUILDINGS.CONSTRUCTION_MASS_KG.TIER0, MATERIALS.REFINED_METALS, 
			1600f, BuildLocationRule.Anywhere, BUILDINGS.DECOR.PENALTY.TIER0, NOISE_POLLUTION.NONE,
			0.2f);
		
		def.ViewMode = OverlayModes.Logic.ID;
		def.ObjectLayer = ObjectLayer.LogicGate;
		def.SceneLayer = Grid.SceneLayer.LogicGates;
		def.ThermalConductivity = 0.05f;
		def.Floodable = false;
		def.Overheatable = false;
		def.Entombable = false;
		def.AudioCategory = "Metal";
		def.AudioSize = "small";
		def.BaseTimeUntilRepair = -1f;
		def.PermittedRotations = PermittedRotations.R360;
		def.DragBuild = true;
		
		def.AddSearchTerms(SEARCH_TERMS.AUTOMATION);
		GeneratedBuildings.RegisterWithOverlay(OverlayModes.Logic.HighlightItemIDs, Name);
		return def;
	}

	public override void ConfigureBuildingTemplate(GameObject go, Tag prefabTag){
		GeneratedBuildings.MakeBuildingAlwaysOperational(go);
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefabTag);
	}

	public override void DoPostConfigurePreview(BuildingDef def, GameObject go){
		base.DoPostConfigurePreview(def, go);
		// TODO: is it necessary to configurate gate visualizers?
	}

	public override void DoPostConfigureUnderConstruction(GameObject go){
		base.DoPostConfigureUnderConstruction(go);
		// TODO: is it necessary to configurate gate visualizers?
	}

	public override void DoPostConfigureComplete(GameObject go){
		go.AddComponent<Circuit>();
		go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayBehindConduits);
		CNIUtil.AddBuildingStrings(Name, Name, "Description", "Effect");
		ModUtil.AddBuildingToPlanScreen("Automation", Name, "wires", "LogicWire");
		CNIUtil.Log("Circuit registered successfully: " + Name);
	}
}
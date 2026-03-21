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
	protected virtual string Id => $"Circuit_{Width}x{Height}";
	protected virtual string Anim => $"logic_circuit_{Width}x{Height}_kanim";
	
	public override BuildingDef CreateBuildingDef(){
		var buildingCost = new float[] {
			Width * Height * 15
		};
		
		var def = BuildingTemplates.CreateBuildingDef(Id, Width, Height, Anim, 10,
			3f, buildingCost, MATERIALS.REFINED_METALS, 
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
		GeneratedBuildings.RegisterWithOverlay(OverlayModes.Logic.HighlightItemIDs, Id);
		return def;
	}

	public override void ConfigureBuildingTemplate(GameObject go, Tag prefabTag){
		GeneratedBuildings.MakeBuildingAlwaysOperational(go);
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefabTag);
	}

	public override void DoPostConfigureComplete(GameObject go){
		go.AddComponent<Circuit>();
		go.AddOrGet<CopyBuildingSettings>();
		go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayBehindConduits);
		ModUtil.AddBuildingToPlanScreen("Automation", Id, "logicgates", "LogicGateXOR");
	}
}
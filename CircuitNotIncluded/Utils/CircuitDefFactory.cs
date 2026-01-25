using UnityEngine;
using STRINGS;
using TUNING;
using BUILDINGS = TUNING.BUILDINGS;

namespace CircuitNotIncluded.Utils;

public static class CircuitDefFactory {
	public static BuildingDef Create(string id, int width, int height, string anim){
		var def = BuildingTemplates.CreateBuildingDef(id, width, height, anim, 10,
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
		GeneratedBuildings.RegisterWithOverlay(OverlayModes.Logic.HighlightItemIDs, id);
		return def;
	}
}
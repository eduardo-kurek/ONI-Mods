using UnityEngine;
using STRINGS;
using TUNING;
using BUILDINGS = TUNING.BUILDINGS;

namespace CircuitNotIncluded.Utils;

public static class CircuitDefFactory {
	public static BuildingDef Create(string id, int width, int height, string anim){
		var def = ScriptableObject.CreateInstance<BuildingDef>();
		
		def.PrefabID = id;
		def.InitDef();
		def.name = id;
		def.Mass = BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		def.MassForTemperatureModification = BUILDINGS.CONSTRUCTION_MASS_KG.TIER3[0] * 0.2f;
		def.WidthInCells = width;
		def.HeightInCells = height;
		def.HitPoints = 30;
		def.ConstructionTime = 30f;
		def.SceneLayer = Grid.SceneLayer.LogicGates;
		def.MaterialCategory = MATERIALS.REFINED_METALS;
		def.BaseMeltingPoint = 1600f;
		def.ContinuouslyCheckFoundation = false;
		def.BuildLocationRule = BuildLocationRule.Anywhere;
		def.ObjectLayer = ObjectLayer.LogicGate;
		def.AnimFiles = [ Assets.GetAnim(anim) ];
		def.GenerateOffsets();
		def.BaseDecor = BUILDINGS.DECOR.PENALTY.TIER0.amount;
		def.BaseDecorRadius = BUILDINGS.DECOR.PENALTY.TIER0.radius;
		def.BaseNoisePollution = NOISE_POLLUTION.NONE.amount;
		def.BaseNoisePollutionRadius = NOISE_POLLUTION.NONE.radius;
		
		def.ViewMode = OverlayModes.Logic.ID;
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
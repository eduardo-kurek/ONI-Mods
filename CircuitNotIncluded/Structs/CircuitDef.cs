using TUNING;

namespace CircuitNotIncluded.Structs;

public class CircuitDef : BuildingDef {
	
	public string CNI_Name = null!;
	public string CNI_Description = null!;
	public string CNI_Effect = null!;
	public List<CNIPort> CNI_InputPorts = null!;
	public List<Output> CNI_Outputs = null!;
	
	public static CircuitDef Create(string name, string description, string effect, int width, int height, string anim, 
			List<CNIPort> inputPorts, List<Output> outputs)
	{
		string id = "cni_" + name;
		id = id.Trim();
		id = id.Replace(" ", "");
		
		CircuitDef circuitDef = CreateInstance<CircuitDef>();
		circuitDef.CNI_Name = name;
		circuitDef.CNI_Description = description;
		circuitDef.CNI_Effect = effect;
		circuitDef.CNI_Outputs = outputs;
		circuitDef.CNI_InputPorts = inputPorts;
		
		circuitDef.PrefabID = id;
		circuitDef.InitDef();
		circuitDef.name = id;
		circuitDef.Mass = BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		circuitDef.MassForTemperatureModification = BUILDINGS.CONSTRUCTION_MASS_KG.TIER3[0] * 0.2f;
		circuitDef.WidthInCells = width;
		circuitDef.HeightInCells = height;
		circuitDef.HitPoints = 30;
		circuitDef.ConstructionTime = 30f;
		circuitDef.SceneLayer = Grid.SceneLayer.LogicGates;
		circuitDef.MaterialCategory = MATERIALS.REFINED_METALS;
		circuitDef.BaseMeltingPoint = 1600f;
		circuitDef.ContinuouslyCheckFoundation = false;
		circuitDef.BuildLocationRule = BuildLocationRule.Anywhere;
		circuitDef.ObjectLayer = ObjectLayer.LogicGate;
		circuitDef.AnimFiles = new[] { Assets.GetAnim(anim) };
		circuitDef.GenerateOffsets();
		circuitDef.BaseDecor = BUILDINGS.DECOR.PENALTY.TIER0.amount;
		circuitDef.BaseDecorRadius = BUILDINGS.DECOR.PENALTY.TIER0.radius;
		circuitDef.BaseNoisePollution = NOISE_POLLUTION.NONE.amount;
		circuitDef.BaseNoisePollutionRadius = NOISE_POLLUTION.NONE.radius;
		
		circuitDef.ViewMode = OverlayModes.Logic.ID;
		circuitDef.ThermalConductivity = 0.05f;
		circuitDef.Floodable = false;
		circuitDef.Overheatable = false;
		circuitDef.Entombable = false;
		circuitDef.AudioCategory = "Metal";
		circuitDef.AudioSize = "small";
		circuitDef.BaseTimeUntilRepair = -1f;
		circuitDef.PermittedRotations = PermittedRotations.R360;
		circuitDef.DragBuild = true;
		
		return circuitDef;
	}
	
}
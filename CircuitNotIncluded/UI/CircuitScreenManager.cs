using CircuitNotIncluded.Structs;
using CircuitNotIncluded.UI.Cells;
using CircuitNotIncluded.UI.Validators;
using CircuitNotIncluded.Utils;
using PeterHan.PLib.Core;
using UnityEngine;

namespace CircuitNotIncluded.UI;

public partial class CircuitScreenManager {
	private Circuit circuit = null!;
	private CircuitScreen screen = null!;
	
	public static CircuitScreenManager Instance { get; } = new ();
	private CircuitScreenManager(){ }
	
	public GameObject Build(Circuit circuit){
		this.circuit = circuit;
		GameObject go = new GameObject("CircuitScreen")
			.RectTransform()
			.AnchorMin(0, 0)
			.AnchorMax(1, 1)
			.OffsetMin(0, 0)
			.OffsetMax(0, 0).gameObject
			.SetParent(RootParent);

		screen = go.AddComponent<CircuitScreen>();
		screen.OnSave += SaveCircuit; 
		BuildScreen(go);
		return go;
	}
	
	private void SaveCircuit(List<InputCellType> inputs, List<OutputCellType> outputs){
		ValidationContext ctx = PortHandler.Validate(inputs, outputs);
		var inputsResult = ctx.ResultInput();
		var outputsResult = ctx.ResultOutput();
		circuit.SetPorts(inputsResult, outputsResult);
	}
	
	public void OnOutputCellCreated(OutputCellType data) => screen.OnOutputCellCreated(data);
	public void OnOutputCellDeleted(OutputCellType data) => screen.OnOutputCellDeleted(data);
	public void OnInputCellCreated(InputCellType data) => screen.OnInputCellCreated(data);
	public void OnInputCellDeleted(InputCellType data) => screen.OnInputCellDeleted(data);
	public void OnCellClicked(CircuitCellType data) => screen.OnCellClicked(data);
}
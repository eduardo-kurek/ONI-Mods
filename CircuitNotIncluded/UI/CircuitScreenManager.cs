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
	private string circuitNameBuffer = null!;
	
	public static CircuitScreenManager Instance { get; } = new ();
	private CircuitScreenManager(){ }
	
	public GameObject Build(Circuit circuit){
		this.circuit = circuit;
		this.circuitNameBuffer = circuit.CNIName;
		GameObject go = new GameObject("CircuitScreen")
			.RectTransform()
			.AnchorMin(0, 0)
			.AnchorMax(1, 1)
			.OffsetMin(0, 0)
			.OffsetMax(0, 0).gameObject
			.SetParent(RootParent);

		screen = go.AddComponent<CircuitScreen>();
		screen.onSave += SaveCircuit; 
		BuildScreen(go);
		return go;
	}
	
	private void SaveCircuit(List<InputCellState> inputs, List<OutputCellState> outputs){
		Debug.Log("Saving circuit...");
		ValidationContext ctx = PortHandler.Validate(inputs, outputs);
		var inputsResult = ctx.ResultInput();
		var outputsResult = ctx.ResultOutput();
		circuit.SetPorts(inputsResult, outputsResult);
		Debug.Log("None errors found.");
		
	}
	
	public void OnOutputCellCreated(OutputCellState data) => screen.AddOutputCell(data);
	public void OnOutputCellDeleted(OutputCellState data) => screen.RemoveOutputCell(data);
	public void OnInputCellCreated(InputCellState data) => screen.AddInputCell(data);
	public void OnInputCellDeleted(InputCellState data) => screen.RemoveInputCell(data);
	public void BuildSideScreen(CircuitCellState data) => screen.OnCellClicked(data);
}
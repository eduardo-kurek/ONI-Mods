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
		circuitNameBuffer = circuit.CNIName;
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
		ValidationContext ctx = Validator.Validate(inputs, outputs);
		
		List<OutputPort> resultOutput = [];
		resultOutput.AddRange(outputs.Select(i => i.ToPort()));
		
		List<InputPort> resultInput = [];
		resultInput.AddRange(inputs.Select(i => i.ToPort()));
		
		circuit.SetData(circuitNameBuffer, resultInput, resultOutput);
	}
	
	public void OnOutputCellCreated(OutputCellState data) => screen.AddOutputCell(data);
	public void OnOutputCellDeleted(OutputCellState data) => screen.RemoveOutputCell(data);
	public void OnInputCellCreated(InputCellState data) => screen.AddInputCell(data);
	public void OnInputCellDeleted(InputCellState data) => screen.RemoveInputCell(data);
	public void BuildSideScreen(CircuitCellState data) => screen.OnCellClicked(data);
}
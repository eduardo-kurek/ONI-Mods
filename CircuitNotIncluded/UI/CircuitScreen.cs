using CircuitNotIncluded.Structs;
using CircuitNotIncluded.Structs.Ports;
using CircuitNotIncluded.UI.Cells;
using CircuitNotIncluded.UI.Validators;
using PeterHan.PLib.Core;
using PeterHan.PLib.UI;
using UnityEngine;
using UnityEngine.UI;

namespace CircuitNotIncluded.UI;

public class CircuitScreen : KModalScreen {
	private readonly List<InputCellState> InputCellTypes = [];
	private readonly List<OutputCellState> OutputCellTypes = [];
	
	public LocText title = null!;
	public GameObject editorContent = null!;
	public GameObject displayCellGrid = null!;
	public delegate void OnSave(InputPort[] inputPorts, OutputPort[] outputPorts);
	public OnSave? onSave;

	public void OnReady(){
		// TODO: snapshot validation logic
	}

	public void RemoveInputCell(InputCellState data) => InputCellTypes.Remove(data);
	public void AddInputCell(InputCellState data) => InputCellTypes.Add(data);
	public void RemoveOutputCell(OutputCellState data) => OutputCellTypes.Remove(data);
	public void AddOutputCell(OutputCellState data) => OutputCellTypes.Add(data);
	
	public void OnCellClicked(CircuitCellState cellState){
		GameObject content = cellState.BuildEditorContent();
		foreach(Transform child in editorContent.transform)
			Destroy(child.gameObject);
		content.SetParent(editorContent);
	}

	private bool IsEmpty() => OutputCellTypes.Count == 0 && InputCellTypes.Count == 0;
	
	public void SaveButtonClicked(){
		if(!Changed()){
			SaveCells();
			return;
		}
		
		string errMsg = ValidateCells();
		if(errMsg != string.Empty){
			ShowMessageDialog(errMsg);
			return;
		}
		
		PUIElements.ShowConfirmDialog(CircuitScreenManager.RootParent,
									"Are you sure you want to apply all changes?",
									SaveCells,
									() => {},
									"Yes",
									"No");
	}
	
	private bool Changed() {
		// TODO: changed logic based on snapshot
		return false;
	}
	
	private string ValidateCells() {
		try {
			Validator.Validate(InputCellTypes, OutputCellTypes);
			return string.Empty;
		} catch (Exception e){
			return e.Message;
		}
	}
	
	private static void ShowMessageDialog(string message){
		var dialog = PUIElements.ShowMessageDialog(CircuitScreenManager.RootParent, message);
		Transform go = dialog.transform.GetChild(1);
		go.GetComponent<LayoutElement>().preferredWidth = 600;
		go.GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
		Transform cancelButton = go.GetChild(3).GetChild(2);
		Destroy(cancelButton.gameObject);
	}
	
	private void SaveCells() {
		List<InputPort> inputs = [];
		inputs.AddRange(InputCellTypes.Select(i => i.ToPort()));
		
		List<OutputPort> outputs = [];
		outputs.AddRange(OutputCellTypes.Select(i => i.ToPort()));
		
		onSave?.Invoke([..inputs], [..outputs]);
		Deactivate();
	}
	
	public void ClearButtonClicked() {
		if(!IsEmpty()){
			PUIElements.ShowConfirmDialog(CircuitScreenManager.RootParent,
				"Are you sure you want to clear all cells?",
				ClearCells,
				()=>{},
				"Yes",
				"No");	
		}
	}
	
	private void ClearCells() {
		foreach (var cell in InputCellTypes.ToList()) cell.Delete();
		foreach (var cell in OutputCellTypes.ToList()) cell.Delete();
	}
	
	public void CancelButtonClicked() {
		if (Changed()) {
			PUIElements.ShowConfirmDialog(CircuitScreenManager.RootParent, 
				"Are you sure you want to leave and discard changes?",
				Deactivate,
				()=> {},
				"Yes",
				"No");
		} else {
			Deactivate();
		}
	}
	
	protected override void OnDeactivate(){
		CircuitCell.Selected = null;
	}
} 
using CircuitNotIncluded.UI.Cells;
using PeterHan.PLib.Core;
using PeterHan.PLib.UI;
using UnityEngine;
using UnityEngine.UI;

namespace CircuitNotIncluded.UI;

public class CircuitScreen : KModalScreen {
	private readonly List<InputCellState> InputCellTypes = [];
	private readonly List<OutputCellState> OutputCellTypes = [];
	
	private List<InputCellState> InputCellTypesSnapshot = [];
	private List<OutputCellState> OutputCellTypesSnapshot = [];
	
	public LocText title = null!;
	public GameObject editorContent = null!;
	public GameObject displayCellGrid = null!;
	public delegate void OnSave(List<InputCellState> inputs, List<OutputCellState> outputs);
	public OnSave? onSave;

	public void OnReady() { 
		InputCellTypesSnapshot = InputCellTypes.ToList();
		OutputCellTypesSnapshot = OutputCellTypes.ToList();
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

	private bool Changed() {
		if (!InputCellTypesSnapshot.SequenceEqual(InputCellTypes)) return true;
		if (!OutputCellTypesSnapshot.SequenceEqual(OutputCellTypes)) return true;
		return false;
	}

	private bool IsEmpty() {
		if ((OutputCellTypes.Count == 0) && (InputCellTypes.Count == 0))
			return true;
		return false;	
	}
	
	public void SaveButtonClicked(){
		if (Changed()) {
			PUIElements.ShowConfirmDialog(CircuitScreenManager.RootParent,
										"Are you sure you want to apply all changes?",
										() => SaveCells(),
										() => {},
										"Yes",
										"No");
		} else {
			Deactivate();	
		}
	}
	 
	private void SaveCells() {
		try{
			onSave?.Invoke(InputCellTypes, OutputCellTypes);
			Deactivate();
		} catch (Exception e){
			ShowMessageDialog(e.Message);
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
	
	public void CancelButtonClicked() {
		if (Changed()) {
			PUIElements.ShowConfirmDialog(CircuitScreenManager.RootParent, 
										"Are you sure you want to leave and discard changes?",
										() => Deactivate(),
										()=> {},
										"Yes",
										"No");
		} else {
			Deactivate();
		}
	}
	
	public void ClearButtonClicked() {
		if(!IsEmpty()){
			PUIElements.ShowConfirmDialog(CircuitScreenManager.RootParent,
										"Are you sure you want to clear all cells?",
										() => ClearCells(),
										()=>{},
										"Yes",
										"No");	
		}
	}
	
	private void ClearCells() {
		foreach (var cell in InputCellTypes.ToList()) cell.Delete();
		foreach (var cell in OutputCellTypes.ToList()) cell.Delete();
	}

	protected override void OnDeactivate(){
		CircuitCell.Selected = null;
	}
} 
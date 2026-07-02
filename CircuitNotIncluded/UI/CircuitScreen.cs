using CircuitNotIncluded.Core.DTO;
using CircuitNotIncluded.Core.Model;
using CircuitNotIncluded.Core.Validators;
using CircuitNotIncluded.UI.Cells;
using PeterHan.PLib.Core;
using PeterHan.PLib.UI;
using UnityEngine;
using UnityEngine.UI;

namespace CircuitNotIncluded.UI;

public class CircuitScreen : KModalScreen {
	public OffsetResolver resolver = null!;
	public string CircuitName = "Circuit name";
	private readonly List<PortCellState> PortCellStates = [];

	private CircuitDTO snapshot = null!;
	
	public LocText title = null!;
	public GameObject editorContent = null!;
	public GameObject displayCellGrid = null!;
	public delegate void OnSave(CircuitDTO circuitDto);
	public OnSave? onSave;

	public void OnReady(){
		snapshot = GetValue();
		Console.WriteLine($"Snapshot: {snapshot}");
	}

	public void AddPortCell(PortCellState cellState){ PortCellStates.Add(cellState); }
	public void RemovePortCell(PortCellState cellState){ PortCellStates.Remove(cellState); }

	public void OnCellClicked(CircuitCellState cellState){
		GameObject content = cellState.BuildEditorContent();
		foreach(Transform child in editorContent.transform)
			Destroy(child.gameObject);
		content.SetParent(editorContent);
	}

	private bool IsEmpty() => PortCellStates.Count == 0;

	private CircuitDTO GetValue(){
		var portDtos = PortCellStates.Select(p => p.CreateDTO()).ToArray();
		var inputs = portDtos.OfType<InputPortDTO>().ToArray();
		var outputs = portDtos.OfType<OutputPortDTO>().ToArray();
		return new CircuitDTO(CircuitName, inputs, outputs);
	}
	
	public void SaveButtonClicked(){
		try {
			if(!HasChanged(out CircuitDTO dto)){
				ExecuteSave(dto);
				return;
			}
     
			CircuitValidator.DoValidate(new CircuitModel(dto, resolver));
     
			PUIElements.ShowConfirmDialog(
				parent: CircuitScreenManager.RootParent,
				message: "Are you sure you want to apply all changes?",
				onConfirm: () => ExecuteSave(dto),
				onCancel: () => {},
				confirmText: "Yes",
				cancelText: "No"
			);
		}
		catch (Exception e) {
			ShowMessageDialog(e.Message);
		}
	}

	private bool HasChanged(out CircuitDTO dto){
		dto = GetValue();
		return !dto.Equals(snapshot);
	}

	private bool HasChanged() => HasChanged(out _);

	private void ExecuteSave(CircuitDTO dto){
		onSave?.Invoke(dto);   
		Deactivate();
	}
	
	private static void ShowMessageDialog(string message){
		var dialog = PUIElements.ShowMessageDialog(CircuitScreenManager.RootParent, message);
		Transform go = dialog.transform.GetChild(1);
		go.GetComponent<LayoutElement>().preferredWidth = 600;
		go.GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
		Transform cancelButton = go.GetChild(3).GetChild(2);
		Destroy(cancelButton.gameObject);
	}
	
	public void ClearButtonClicked(){
		if(!IsEmpty()){
			PUIElements.ShowConfirmDialog(CircuitScreenManager.RootParent,
				"Are you sure you want to clear all cells?",
				ClearCells,
				null,
				"Yes",
				"No");	
		}
	}
	
	private void ClearCells(){
		foreach (var cell in PortCellStates.ToArray()) cell.Delete();
	}
	
	public void CancelButtonClicked(){
		if (HasChanged()) {
			PUIElements.ShowConfirmDialog(CircuitScreenManager.RootParent, 
				"Are you sure you want to leave and discard changes?",
				Deactivate,
				null,
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
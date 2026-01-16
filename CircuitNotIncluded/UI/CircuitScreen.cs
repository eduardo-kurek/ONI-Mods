using CircuitNotIncluded.UI.Cells;
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
	public delegate void OnSave(List<InputCellState> inputs, List<OutputCellState> outputs);
	public OnSave? onSave;
	
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

	public void SaveButtonClicked(){
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
	
	public void CancelButtonClicked(){
		Deactivate();
	}

	protected override void OnDeactivate(){
		CircuitCell.Selected = null;
	}
}
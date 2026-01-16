using System.Text.RegularExpressions;
using CircuitNotIncluded.Grammar;
using CircuitNotIncluded.Structs;
using CircuitNotIncluded.UI.Cells;
using CircuitNotIncluded.UI.Validators;
using CircuitNotIncluded.Utils;
using PeterHan.PLib.Core;
using PeterHan.PLib.UI;
using ProcGen.Map;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CircuitNotIncluded.UI;

public class CircuitScreen : KModalScreen {
	private readonly List<InputCellType> InputCellTypes = [];
	private readonly List<OutputCellType> OutputCellTypes = [];
	
	public LocText title = null!;
	public GameObject editorContent = null!;
	public GameObject displayCellGrid = null!;

	public event Action<List<InputCellType>, List<OutputCellType>>? OnSave;
	
	public void OnInputCellDeleted(InputCellType data){  InputCellTypes.Remove(data); }
	public void OnInputCellCreated(InputCellType data){ InputCellTypes.Add(data); }
	public void OnOutputCellDeleted(OutputCellType data){ OutputCellTypes.Remove(data); }
	public void OnOutputCellCreated(OutputCellType data){ OutputCellTypes.Add(data); }
	
	public void OnCellClicked(CircuitCellType cellType){
		GameObject content = cellType.BuildEditorContent();
		foreach(Transform child in editorContent.transform)
			Destroy(child.gameObject);
		content.SetParent(editorContent);
	}

	public void SaveButtonClicked(){
		try{
			OnSave?.Invoke(InputCellTypes, OutputCellTypes);
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
using System.Text.RegularExpressions;
using CircuitNotIncluded.Grammar;
using CircuitNotIncluded.Structs;
using CircuitNotIncluded.UI.Cells;
using CircuitNotIncluded.Utils;
using PeterHan.PLib.Core;
using PeterHan.PLib.UI;
using ProcGen.Map;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CircuitNotIncluded.UI;

public class CircuitScreen : KModalScreen
{
	public static CircuitScreen Instance = null!;
	public static List<InputCellType> InputCellTypes = [];
	public static List<OutputCellType> OutputCellTypes = [];
	
	public Circuit Circuit = null!;
	public LocText title = null!;
	public GameObject editorContent = null!;
	public GameObject displayCellGrid = null!;

	public CircuitScreen Initialize(Circuit circuit){
		Circuit = circuit;
		Instance = this;
		return this;
	}

	public void OnCellClicked(CircuitCellType cellType){
		GameObject content = cellType.BuildEditorContent();
		ChangeEditorContent(content);
	}

	private void ChangeEditorContent(GameObject content){
		ClearEditorTab();
		content.SetParent(editorContent);
	}

	private void ClearEditorTab(){
		foreach(Transform child in editorContent.transform){
			Destroy(child.gameObject);
		}
	}

	public void SaveButtonClicked(){
		try{
			var inputs = CheckInputPorts();
			var outputs = CheckOutputPorts();
			Circuit.Refresh(inputs, outputs);
			Deactivate();
		} catch (Exception e){
			var dialog = PUIElements.ShowMessageDialog(CircuitScreenBuilder.GetRootParent(), e.Message);
			GameObjectDebugger.PrintGameObjectInfo(dialog.gameObject, true, 3);
			var go = dialog.transform.GetChild(1);
			go.GetComponent<LayoutElement>().preferredWidth = 600;
			go.GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
			
			// Deleting Cancel Button
			var cancelButton = go.GetChild(3).GetChild(2);
			Destroy(cancelButton.gameObject);
		}
	}
	
	private static List<CNIPort> CheckInputPorts(){
		Dictionary<string, CellOffset> ids = [];
		string errMessage = "";
		
		foreach(InputCellType i in InputCellTypes){
			string id = i.GetId();
			
			if(id.IsNullOrWhiteSpace()){
				errMessage += $"({i.X()}, {i.Y()}). Input id cannot be empty.\n";
				continue;
			}

			if(id.Contains(" ")){
				errMessage += $"({i.X()}, {i.Y()}). Input id cannot contains spaces.\n";
				continue;
			}
			
			if(!char.IsLetter(id[0]) && id[0] != '_'){
				errMessage += $"({i.X()}, {i.Y()}). Input id must start with a letter or underline.\n";
				continue;
			}
			
			for(int j = 1; j < id.Length; j++){
				if (!Regex.IsMatch(id[j].ToString(), @"^[_a-zA-Z0-9]$")){
					errMessage += $"({i.X()}, {i.Y()}). Input id has an invalid char at. {id[j]} must be a letter, underscore or number\n";
				}
			}
			
			if(ids.ContainsKey(i.GetId())){
				CellOffset oldOffset = ids[i.GetId()];
				CellOffset newOffset = i.GetOffset();
				errMessage += $"({newOffset.x}, {newOffset.y}). Duplicated input id: {i.GetId()}." +
				              $"Already declared in ({oldOffset.x}, {oldOffset.y})\n";
				continue;
			}
			ids.Add(i.GetId(), i.GetOffset());
		}

		if(errMessage != "") throw new Exception(errMessage);

		List<CNIPort> inputs = [];
		inputs.AddRange(InputCellTypes.Select(i => i.ToPort()));
		return inputs;
	}

	private static List<Output> CheckOutputPorts(){
		string errMessage = "";
		List<Output> outs = [];
		HashSet<string> inputIds = [];

		foreach(InputCellType input in InputCellTypes){
			inputIds.Add(input.GetId());
		}

		foreach(OutputCellType outputCell in OutputCellTypes){
			try{
				Output output = outputCell.ToPort();
				SemanticAnalyzer.Analyze(output, inputIds);
				outs.Add(output);
			}
			catch(Exception e){
				errMessage += $"({outputCell.X()}, {outputCell.Y()}). {e.Message}\n";
			}
		}
		
		if(errMessage != "") throw new Exception(errMessage);

		return outs;
	}
	
	public void CancelButtonClicked(){
		Debug.Log("Canceling state");
		Deactivate();
	}

	protected override void OnDeactivate(){
		CircuitCell.Selected = null;
		OutputCellType.ResetAutomaticPortIds();
		InputCellTypes.Clear();
		OutputCellTypes.Clear();
	}
}
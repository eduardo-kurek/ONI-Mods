using CircuitNotIncluded.Structs;
using CircuitNotIncluded.Utils;
using PeterHan.PLib.UI;
using UnityEngine;
using UnityEngine.UI;

namespace CircuitNotIncluded.UI.Cells;

public class EmptyCellType(CellOffset offset) : CircuitCellType(offset) {
	protected override string GetCellTitle(){ return "Empty Cell"; }

	public override void UpdateImage(Image img){
		img.color = Color.grey;
		img.sprite = null;
	}

	public override GameObject BuildContainer(){
		return base.BuildContainer()
			.VerticalLayoutGroup()
			.Spacing(10)
			.gameObject;
	}
	
	public override GameObject BuildEditorContent(){
		GameObject mainPanel = BuildContainer();
		GameObject buttonsPanel = BuildButtonsPanel(mainPanel);
		BuildCreateInputButton(buttonsPanel);
		BuildCreateOutputButton(buttonsPanel);
		return mainPanel;
	}

	private static GameObject BuildButtonsPanel(GameObject container){
		return new PPanel()
			.FlexSize(1, 1)
			.Spacing(10)
			.Alignment(TextAnchor.MiddleCenter)
			.AddTo(container);
	}

	private GameObject BuildCreateInputButton(GameObject container){
		return BuildButton(container, "Create Input Port", (go) => PromoteToInput());
	}
	
	private GameObject BuildCreateOutputButton(GameObject container){
		return BuildButton(container, "Create Output Port", (go) => PromoteToOutput());
	}
	
	private static GameObject BuildButton(GameObject container, string text, PUIDelegates.OnButtonPressed onClick){
		return new PButton()
			.Text(text)
			.Margin(10)
			.SetOnClick(onClick)
			.AddTo(container);
	}
	
	private void PromoteToInput(){
		var type = InputCellType.Create(offset);
		parent.SetCellType(type);
		parent.OnPointerClick(null!);
		CircuitScreenManager.Instance.OnInputCellCreated(type);
	}

	private void PromoteToOutput(){
		var type = OutputCellType.Create(offset);
		parent.SetCellType(type);
		parent.OnPointerClick(null!);
		CircuitScreenManager.Instance.OnOutputCellCreated(type);
	}
}
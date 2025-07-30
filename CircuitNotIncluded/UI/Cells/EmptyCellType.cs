using CircuitNotIncluded.Structs;
using CircuitNotIncluded.Utils;
using PeterHan.PLib.UI;
using UnityEngine;
using UnityEngine.UI;

namespace CircuitNotIncluded.UI.Cells;

public class EmptyCellType(CellOffset offset) : CircuitCellType(offset) {
	protected override GameObject BuildContainer(){
		return base.BuildContainer()
			.VerticalLayoutGroup()
			.Spacing(10)
			.gameObject;
	}
	
	public override GameObject BuildEditorContent(){
		GameObject mainPanel = BuildContainer();
		BuildCoordsLabel(mainPanel);
		GameObject buttonsPanel = BuildButtonsPanel(mainPanel);
		BuildCreateInputButton(buttonsPanel);
		BuildCreateOutputButton(buttonsPanel);
		return mainPanel;
	}

	private GameObject BuildCoordsLabel(GameObject container){
		return new PLabel("Coords")
			.Text($"Empty cell ({offset.x}, {offset.y})")
			.Style(CircuitCell.TitleStyle)
			.FlexSize(1, 0)
			.AddTo(container);
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
		InputCellData data = new () { id = "id" };
		InputCellType type = new(data, offset);
		CircuitScreen.InputCellTypes.Add(type);
		parent.SetCellType(type);
		parent.OnPointerClick(null!);
	}

	private void PromoteToOutput(){
		OutputCellData data = new ();
		OutputCellType type = new(data, offset);
		CircuitScreen.OutputCellTypes.Add(type);
		parent.SetCellType(type);
		parent.OnPointerClick(null!);
	}
}
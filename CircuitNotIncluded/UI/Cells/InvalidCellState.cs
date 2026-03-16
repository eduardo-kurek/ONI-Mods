using CircuitNotIncluded.Utils;
using PeterHan.PLib.UI;
using UnityEngine;
using UnityEngine.UI;

namespace CircuitNotIncluded.UI.Cells;

public class InvalidCellState : CircuitCellState {
	protected override string CellTitle => "Invalid Cell";
	
	public override void UpdateCellImage(Image img){
		img.color = new Color(145/255f, 41/255f, 41/255f);
	}
	
	public override GameObject BuildEditorContent(){
		GameObject mainContainer = BuildContainer();
		GameObject textContainer = BuildTextContainer(mainContainer);
		BuildInformativeText(textContainer);
		return mainContainer;
	}

	private static GameObject BuildTextContainer(GameObject container){
		return new PPanel()
			.FlexSize(1, 1)
			.Spacing(10)
			.Alignment(TextAnchor.MiddleCenter)
			.AddTo(container);
	}

	private static GameObject BuildInformativeText(GameObject container){
		return new PLabel("Invalid Cell")
			.Text("There is an obstacle in this cell.\nIt cannot be used for ports.")
			.Style(CircuitCell.LabelStyle)
			.AddTo(container);
	}
}
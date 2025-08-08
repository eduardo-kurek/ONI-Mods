using CircuitNotIncluded.Utils;
using PeterHan.PLib.UI;
using UnityEngine;
using UnityEngine.UI;

namespace CircuitNotIncluded.UI.Cells;

public class InvalidCellType(CellOffset offset) : CircuitCellType(offset) {
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
	
	public override void UpdateImage(Image img){
		img.sprite = null;
		img.color = Color.magenta;
	}
	protected override string GetCellTitle() => "Invalid Cell";
}
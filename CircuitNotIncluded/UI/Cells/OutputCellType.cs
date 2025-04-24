using CircuitNotIncluded.Structs;
using PeterHan.PLib.UI;
using UnityEngine;

namespace CircuitNotIncluded.UI.Cells;

public class OutputCellType(Output output) : CircuitCellType {
	private readonly Output output = output;
	public override GameObject BuildEditorContent(){
		var panel = BuildContainer();
		
		var label = new PLabel("Label") {
			Text = "Output: ",
			TextStyle = PUITuning.Fonts.TextDarkStyle,
			FlexSize = new Vector2(1, 0)
		};
		label.AddTo(panel);
		
		return panel;
	}
	public int GetIndex() => CircuitScreen.Instance.Circuit.ToLinearIndex(output.Port);
}
using CircuitNotIncluded.Structs;
using CircuitNotIncluded.UI.Cells;
using CircuitNotIncluded.Utils;
using PeterHan.PLib.Core;
using PeterHan.PLib.UI;
using UnityEngine;
using UnityEngine.UI;
using static SetTextStyleSetting;

namespace CircuitNotIncluded.UI;

public static class CircuitScreenBuilder
{
	
	private static GameObject? parent;
	private static TextStyleSetting? textStyleSetting;
	
	private const int PORT_SIZE = 40;
	private const int PORT_SPACING = 8;
	
	private static float CircuitDisplaySize(int qtCells){
		return qtCells * PORT_SIZE + PORT_SPACING * (qtCells + 1);
	}

	public static GameObject GetRootParent(){
		if(parent == null)
			parent = GameObject.Find("MiddleCenter");
		return parent;
	}

	private static CircuitScreen Instance(){ return CircuitScreen.Instance; }
	private static Circuit Circuit(){ return Instance().Circuit; }
	private static TextStyleSetting GetTextStyleSetting(){
		if(textStyleSetting == null){
			textStyleSetting = PUITuning.Fonts.UILightStyle;
			textStyleSetting.fontSize = 15;
		}
		return textStyleSetting;
	} 
	
	public static GameObject Build(Circuit circuit){
		UnityEngine.Debug.Log("Circuit BuildingCircuit");
		GameObject go = new GameObject("CircuitScreen")
			.RectTransform()
			.AnchorMin(0, 0)
			.AnchorMax(1, 1)
			.OffsetMin(0, 0)
			.OffsetMax(0, 0).gameObject
			.AddComponent<CircuitScreen>().Initialize(circuit).gameObject
			.SetParent(GetRootParent());
		BuildScreen(go);
		return go;
	}
	
	private static void BuildScreen(GameObject go){
		GameObject container = BuildMainContainer(go.gameObject);
		BuildHeader(container);
		BuildBody(container);
		BuildFooter(container);
	}
	
	private static GameObject BuildMainContainer(GameObject parent){
		return new PPanel()
			.SetKleiBlueColor()
			.DynamicSize(true)
			.FlexSize(1, 1)
			.AddTo(parent)
			.AddOutline()
			.RectTransform()
			.SizeDelta(1100, 700)
			.AnchorMin(0.5f, 0.5f)
			.AnchorMax(0.5f, 0.5f)
			.LocalPosition(Vector3.zero)
			.LocalScale(Vector3.one)
			.gameObject;
	}
	
	private static void BuildHeader(GameObject container){
		GameObject header = BuildHeaderContainer(container);
		GameObject title = BuildHeaderTitle(header);
		Instance().title = header.GetComponentInChildren<LocText>();
	}

	private static GameObject BuildHeaderContainer(GameObject container){
		return new PPanel("Header")
			.SetKleiPinkColor()
			.AddTo(container)
			.AddOutline()
			.LayoutElement()
			.FlexibleWidth(1)
			.PreferredHeight(20)
			.gameObject;
	}

	private static GameObject BuildHeaderTitle(GameObject container){
		return new PLabel("Title")
			.Text("Circuit name")
			.Margin(10)
			.Style(GetTextStyleSetting())
			.AddTo(container);
	}

	private static void BuildBody(GameObject container){
		GameObject body = new PPanel("Body")
			.Direction(PanelDirection.Horizontal)
			.BackColor(new Color32(28, 32, 38, byte.MaxValue))
			.AddTo(container)
			.AddOutline()
			.LayoutElement()
			.FlexibleWidth(1)
			.FlexibleHeight(1)
			.gameObject;

		BuildDisplayTab(body);
		BuildEditorTab(body);
	}

	private static void BuildDisplayTab(GameObject container){
		GameObject displayTab = new PPanel("Display")
			.AddTo(container)
			.AddOutline()
			.LayoutElement()
			.FlexibleWidth(1)
			.FlexibleHeight(1)
			.gameObject;

		BuildCircuitDisplay(displayTab);
	}

	private static void BuildCircuitDisplay(GameObject container){
		Circuit circuit = CircuitScreen.Instance.Circuit;
		float sizeX = CircuitDisplaySize(circuit.GetWidth());
		float sizeY = CircuitDisplaySize(circuit.GetHeight());

		GameObject grid = new GameObject("CellGrid")
			.LayoutElement()
			.PreferredWidth(sizeX)
			.PreferredHeight(sizeY)
			.gameObject
			.RectTransform()
			.gameObject
			.GridLayoutGroup()
			.CellSize(PORT_SIZE, PORT_SIZE)
			.Spacing(PORT_SPACING, PORT_SPACING)
			.Padding(PORT_SPACING)
			.ChildAlignment(TextAnchor.LowerLeft)
			.StartCorner(GridLayoutGroup.Corner.LowerLeft)
			.StartAxis(GridLayoutGroup.Axis.Horizontal)
			.gameObject
			.Image()
			.Color(Color.red)
			.gameObject
			.SetParent(container);

		Instance().displayCellGrid = grid;
		BuildPorts(grid);
	}

	private static void BuildPorts(GameObject container){
		BuildEmptyPorts(container);
		PromoteInputPorts(container);
		PromoteOutputPorts(container);
	}

	private static void BuildEmptyPorts(GameObject container){
		Circuit c = Circuit();
		for(int i = 0; i < c.GetWidth() * c.GetHeight(); i++){
			var offset = c.ToCellOffset(i);
			EmptyCellType cellType = new(offset);
			BuildNewPort(container, cellType);
		}
	}
	
	private static void BuildNewPort(GameObject container, CircuitCellType cellType){
		new GameObject("Cell")
			.RectTransform().gameObject
			.CircuitCell().SetCellType(cellType).gameObject
			.SetParent(container);
	}
	
	private static void PromoteInputPorts(GameObject container){
		Circuit c = Circuit();
		foreach(CNIPort input in c.GetInputPorts()){
			var cellType = InputCellType.Create(input);
			int index = cellType.GetIndex();
			ChangeCellType(index, cellType);
			Instance().OnInputCellCreated(cellType);
		}
	}

	private static void PromoteOutputPorts(GameObject container){
		Circuit c = Circuit();
		foreach(Output output in c.GetOutputs()){
			var cellType = OutputCellType.Create(output);
			int index = cellType.GetIndex();
			ChangeCellType(index, cellType);
			Instance().OnOutputCellCreated(cellType);
		}
	}
	
	private static void ChangeCellType(int index, CircuitCellType cellType){
		GetCellOnGridByIndex(index).SetCellType(cellType);
	}

	private static CircuitCell GetCellOnGridByIndex(int index){
		return Instance().displayCellGrid.transform.GetChild(index).GetComponent<CircuitCell>();
	}
	
	private static void BuildEditorTab(GameObject container){
		GameObject editor = new PPanel("Editor")
			.BackColor(Color.white)
			.AddTo(container)
			.AddOutline()
			.LayoutElement()
			.FlexibleHeight(1)
			.PreferredWidth(350)
			.gameObject;

		GameObject content = BuildEditorContent(editor);
		BuildInfoPanel(editor);
		Instance().editorContent = content;
	}

	private static GameObject BuildEditorContent(GameObject container){
		return new PPanel("EditorContent")
			.Margin(10)
			.AddTo(container)
			.AddOutline()
			.LayoutElement()
			.FlexibleWidth(1)
			.FlexibleHeight(1)
			.gameObject;
	}

	private static GameObject BuildInfoPanel(GameObject container){
		return new PPanel("EditorInfo")
			.SetKleiPinkColor()
			.AddTo(container)
			.AddOutline()
			.LayoutElement()
			.FlexibleWidth(1)
			.PreferredHeight(200)
			.gameObject;
	}
	
	private static void BuildFooter(GameObject container){
		GameObject footer = new PPanel("Footer")
			.Spacing(15)
			.Direction(PanelDirection.Horizontal)
			.Margin(10)
			.AddTo(container)
			.AddOutline()
			.LayoutElement()
			.FlexibleWidth(1)
			.gameObject;

		BuildSaveButton(footer);
		BuildCancelButton(footer);
	}

	private static GameObject BuildSaveButton(GameObject container){
		return BuildButton(container, "Save", (go) => { Instance().SaveButtonClicked(); });
	}

	private static GameObject BuildCancelButton(GameObject container){
		return BuildButton(container, "Cancel", (go) => { Instance().CancelButtonClicked(); });
	}

	private static GameObject BuildButton(GameObject container, string text, PUIDelegates.OnButtonPressed onClick){
		return new PButton()
			.Text(text)
			.Style(GetTextStyleSetting())
			.Margin(10)
			.SetOnClick(onClick)
			.AddTo(container);
	}
}
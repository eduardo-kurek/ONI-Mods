using CircuitNotIncluded.Structs;
using CircuitNotIncluded.UI.Cells;
using CircuitNotIncluded.Utils;
using PeterHan.PLib.Core;
using PeterHan.PLib.UI;
using UnityEngine;
using UnityEngine.UI;

namespace CircuitNotIncluded.UI;

public partial class CircuitScreenManager {
	private const int PORT_SIZE = 40;
	private const int PORT_SPACING = 8;
	private static float CircuitDisplaySize(int qtCells){
		return qtCells * PORT_SIZE + PORT_SPACING * (qtCells + 1);
	}
	
	private static GameObject? parent;
	public static GameObject RootParent {
		get {
			if(parent == null)
				parent = GameObject.Find("MiddleCenter");
			return parent;
		}
	}
	
	private static TextStyleSetting? textStyleSetting;
	private static TextStyleSetting TextStyle {
		get {
			if(textStyleSetting != null) return textStyleSetting;
			textStyleSetting = PUITuning.Fonts.UILightStyle;
			textStyleSetting.fontSize = 15;
			return textStyleSetting;
		}
	}
}


public partial class CircuitScreenManager {
	
	private void BuildScreen(GameObject go){
		GameObject container = BuildMainContainer(go.gameObject);
		BuildHeader(container);
		BuildBody(container);
		BuildFooter(container);
	}
	
	private GameObject BuildMainContainer(GameObject parent){
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
	
	private void BuildHeader(GameObject container){
		GameObject header = BuildHeaderContainer(container);
		GameObject title = BuildHeaderTitle(header);
		screen.title = header.GetComponentInChildren<LocText>();
	}

	private GameObject BuildHeaderContainer(GameObject container){
		return new PPanel("Header")
			.SetKleiPinkColor()
			.AddTo(container)
			.AddOutline()
			.LayoutElement()
			.FlexibleWidth(1)
			.PreferredHeight(20)
			.gameObject;
	}

	private GameObject BuildHeaderTitle(GameObject container){
		return new PLabel("Title")
			.Text("Circuit name")
			.Margin(10)
			.Style(TextStyle)
			.AddTo(container);
	}

	private void BuildBody(GameObject container){
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

	private void BuildDisplayTab(GameObject container){
		GameObject displayTab = new PPanel("Display")
			.AddTo(container)
			.AddOutline()
			.LayoutElement()
			.FlexibleWidth(1)
			.FlexibleHeight(1)
			.gameObject;

		BuildCircuitDisplay(displayTab);
	}

	private void BuildCircuitDisplay(GameObject container){
		float sizeX = CircuitDisplaySize(circuit.Width);
		float sizeY = CircuitDisplaySize(circuit.Height);

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
			.Image().Sprite(circuit.GetCircuitSprite())
			.gameObject
			.SetParent(container);
		
		screen.displayCellGrid = grid;
		BuildPorts(grid);
		Orientation orientation = GetOrientation(circuit.gameObject);
		ApplyOrientation(grid, orientation);
	}

	private void BuildPorts(GameObject container){
		BuildEmptyPorts(container);
		BuildInputPorts(container);
		BuildOutputPorts(container);
		BuildInvalidPorts(container);
	}

	private void BuildEmptyPorts(GameObject container){
		Circuit c = circuit;
		for(int i = 0; i < c.Width * c.Height; i++){
			CellOffset offset = c.ToCellOffset(i);
			EmptyCellState cellState = new();
			BuildNewPort(container, cellState, offset, circuit.ToDisplayIndex(offset));
		}
	}
	
	private static void BuildNewPort(GameObject container, CircuitCellState cellState, CellOffset offset, int displayIndex){
		new GameObject("Cell")
			.RectTransform().gameObject
			.CircuitCell(offset, displayIndex).TransitionTo(cellState).gameObject
			.SetParent(container);
	}
	
	private void BuildInputPorts(GameObject container){
		Circuit c = circuit;
		foreach(InputPort input in c.InputPorts){
			var cellType = InputCellState.Create(input);
			int index = circuit.ToGridIndex(input.Offset);
			ChangeCellType(index, cellType);
		}
	}

	private void BuildOutputPorts(GameObject container){
		Circuit c = circuit;
		foreach(OutputPort output in c.GetOutputs()){
			var cellType = OutputCellState.Create(output);
			int index = circuit.ToGridIndex(output.Offset);
			ChangeCellType(index, cellType);
		}
	}
	
	private void ChangeCellType(int index, CircuitCellState cellState){
		GetCellOnGridByIndex(index).TransitionTo(cellState);
	}
	
	private CircuitCell GetCellOnGridByIndex(int index){
		return screen.displayCellGrid.transform.GetChild(index).GetComponent<CircuitCell>();
	}
	
	private void BuildInvalidPorts(GameObject container){
		var emptyCells = GetEmptyCellsFromGrid(container);
		CreateInvalidPorts(emptyCells);
	}

	private List<CircuitCell> GetEmptyCellsFromGrid(GameObject grid){
		var allCells = grid.GetComponentsInChildren<CircuitCell>().ToList();
		List<CircuitCell> emptyCells = [];
		foreach(CircuitCell cell in allCells){
			CircuitCellState state = cell.GetCellType();
			if(state is EmptyCellState)
				emptyCells.Add(cell);
		}
		return emptyCells;
	}

	private void CreateInvalidPorts(List<CircuitCell> cells){
		foreach(CircuitCell cell in cells){
			if(!CellHasConflict(cell)) continue;
			var invalidType = new InvalidCellState();
			cell.TransitionTo(invalidType);
		}
	}

	private bool CellHasConflict(CircuitCell cell){
		int globalCell = circuit.GetGlobalPositionCell(cell.Offset);
		object endpointInCell = Game.Instance.logicCircuitSystem.GetEndpoint(globalCell);
		return endpointInCell != null;
	}
	
	private void BuildEditorTab(GameObject container){
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
		screen.editorContent = content;
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

	private GameObject BuildInfoPanel(GameObject container){
		return new PPanel("EditorInfo")
			.SetKleiPinkColor()
			.AddTo(container)
			.AddOutline()
			.LayoutElement()
			.FlexibleWidth(1)
			.PreferredHeight(200)
			.gameObject;
	}
	
	private void BuildFooter(GameObject container){
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

	private GameObject BuildSaveButton(GameObject container){
		return BuildButton(container, "Save", (go) => { screen.SaveButtonClicked(); });
	}

	private GameObject BuildCancelButton(GameObject container){
		return BuildButton(container, "Cancel", (go) => { screen.CancelButtonClicked(); });
	}

	private GameObject BuildButton(GameObject container, string text, PUIDelegates.OnButtonPressed onClick){
		return new PButton()
			.Text(text)
			.Style(TextStyle)
			.Margin(10)
			.SetOnClick(onClick)
			.AddTo(container);
	}

	private static Orientation GetOrientation(GameObject go){
		return go.GetComponent<Rotatable>().GetOrientation();
	}

	private void ApplyOrientation(GameObject go, Orientation orientation){
		go.transform.rotation = orientation switch {
			Orientation.R90 => Quaternion.Euler(0f, 0f, -90f),
			Orientation.R180 => Quaternion.Euler(0f, 0f, 180f),
			Orientation.R270 => Quaternion.Euler(0f, 0f, 90f),
			_ => go.transform.rotation
		};
	}
	
}
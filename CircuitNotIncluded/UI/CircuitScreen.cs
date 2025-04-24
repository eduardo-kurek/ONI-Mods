using CircuitNotIncluded.Structs;
using CircuitNotIncluded.UI.Cells;
using PeterHan.PLib.Core;
using PeterHan.PLib.UI;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.GridLayoutGroup;

namespace CircuitNotIncluded.UI;

public class CircuitScreen : KModalScreen
{
	private static GameObject? parent;
	public static CircuitScreen Instance = null!;

	// The size of the port to be displayed on editor
	private const int PORT_SIZE = 60;

	// The spacing between the ports on editor
	private const int PORT_SPACING = 5;

	private static float CircuitDisplaySize(int qtCells){
		return qtCells * PORT_SIZE + PORT_SPACING * (qtCells + 1);
	}

	private static float CircuitDisplaySizePerCell(int qtCells){
		return PORT_SIZE + PORT_SPACING + (float)PORT_SPACING / qtCells;
	}


	public Circuit Circuit = null!;
	private LocText title = null!;
	private GameObject editorContent = null!;
	private GameObject displayCellGrid = null!;

	private static GameObject GetParent(){
		if(parent == null)
			parent = GameObject.Find("MiddleCenter");
		return parent;
	}

	public static GameObject Build(Circuit circuit){
		UnityEngine.Debug.Log("Circuit BuildingCircuit");
		var go = new GameObject("CircuitScreen");
		go.AddComponent<CircuitScreen>().Initialize(circuit);
		var rt = go.AddComponent<RectTransform>();
		rt.anchorMin = Vector2.zero;
		rt.anchorMax = Vector2.one;
		rt.offsetMin = Vector2.zero;
		rt.offsetMax = Vector2.zero;
		go.SetParent(GetParent());
		return go;
	}

	private void Initialize(Circuit circuit){
		Circuit = circuit;
		Instance = this;
		PPanel contentPanel = new PPanel();
		contentPanel.SetKleiBlueColor();
		contentPanel.DynamicSize = true;
		contentPanel.FlexSize = Vector2.one;

		GameObject content = contentPanel.Build();
		var outline = content.AddComponent<Outline>();
		outline.effectColor = Color.black;
		outline.effectDistance = new Vector2(1, 1);

		var rect = content.GetComponent<RectTransform>();
		rect.sizeDelta = new Vector2(1100, 700);
		rect.anchorMin = new Vector2(0.5f, 0.5f);
		rect.anchorMax = new Vector2(0.5f, 0.5f);
		rect.localPosition = Vector3.zero;
		rect.localScale = Vector3.one;

		BuildHeader(content);
		BuildBody(content);
		BuildFooter(content);

		content.SetParent(gameObject);
	}

	private void BuildHeader(GameObject container){
		var textStyle = PUITuning.Fonts.UILightStyle;
		textStyle.fontSize = 15;

		var header = new PPanel("Header");
		var title = new PLabel("Title") {
			Text = "Circuit name",
			Margin = new RectOffset(10, 10, 10, 10),
			TextStyle = textStyle
		};
		header.SetKleiPinkColor();
		header.AddChild(title);

		var obj = header.AddTo(container);
		AddBorder(obj);
		var outline = obj.AddComponent<Outline>();
		outline.effectColor = Color.black;
		outline.effectDistance = new Vector2(1, 1);

		var layout = obj.AddComponent<LayoutElement>();
		layout.flexibleWidth = 1;
		layout.preferredHeight = 20;

		this.title = obj.GetComponentInChildren<LocText>();
	}

	private void BuildBody(GameObject container){
		var body = new PPanel("Body") {
			Direction = PanelDirection.Horizontal,
			BackColor = new Color32(28, 32, 38, byte.MaxValue)
		};

		var obj = body.AddTo(container);
		AddBorder(obj);
		obj.AddOrGet<LayoutElement>().flexibleHeight = 1;
		obj.AddOrGet<LayoutElement>().flexibleWidth = 1;

		BuildDisplayTab(obj);
		BuildEditorTab(obj);
	}

	private void BuildDisplayTab(GameObject container){
		var displayTab = new PPanel("Display");

		var display = displayTab.AddTo(container);
		AddBorder(display);
		display.AddOrGet<LayoutElement>().flexibleWidth = 1;
		display.AddOrGet<LayoutElement>().flexibleHeight = 1;

		BuildCircuitDisplay(display);
	}

	private void BuildCircuitDisplay(GameObject container){
		var child = new GameObject("CellGrid");
		child.AddComponent<RectTransform>();
		var sizeX = CircuitDisplaySize(Circuit.GetWidth());
		var sizeY = CircuitDisplaySize(Circuit.GetHeight());
		child.AddOrGet<LayoutElement>().preferredWidth = sizeX;
		child.AddOrGet<LayoutElement>().preferredHeight = sizeY;
		var grid = child.AddComponent<GridLayoutGroup>();
		grid.cellSize = new Vector2(PORT_SIZE, PORT_SIZE);
		grid.spacing = new Vector2(PORT_SPACING, PORT_SPACING);
		grid.padding = new RectOffset(PORT_SPACING, PORT_SPACING, PORT_SPACING, PORT_SPACING);
		grid.childAlignment = TextAnchor.LowerLeft;
		grid.startCorner = Corner.LowerLeft;
		grid.startAxis = GridLayoutGroup.Axis.Horizontal;

		var img = child.AddComponent<Image>();
		img.color = Color.red;
		child.SetParent(container);
		displayCellGrid = child;
		
		BuildPorts(child);
	}

	private void BuildPorts(GameObject container){
		BuildEmptyPorts(container);
		BuildInputPorts(container);
		BuildOutputPorts(container);
	}

	private void BuildEmptyPorts(GameObject container){
		for(int i = 0; i < Circuit.GetWidth() * Circuit.GetHeight(); i++){
			var cellType = new EmptyCellType("title " + i);
			BuildNewPort(container, cellType);
		}
	}
	
	private void BuildInputPorts(GameObject container){
		foreach(CNIPort input in Circuit.GetInputPorts()){
			Debug.Log("Trying to build input port: " + input.OriginalId);
			Debug.Log("Input port: " + input.P.cellOffset.x + ", " + input.P.cellOffset.y);
			InputCellType cellType = new(input);
			int index = cellType.GetIndex();
			Debug.Log("Input port index: " + index);
			ChangeCellType(index, cellType);
		}
	}

	private void BuildOutputPorts(GameObject container){
		foreach(Output output in Circuit.GetOutputs()){
			OutputCellType cellType = new(output);
			Debug.Log("Trying to build output port: " + output.Port.OriginalId);
			Debug.Log("Output port: " + output.Port.P.cellOffset.x + ", " + output.Port.P.cellOffset.y);
			int index = cellType.GetIndex();
			Debug.Log("Output port index: " + index);
			ChangeCellType(index, cellType);
		}
	}

	private void BuildNewPort(GameObject container, CircuitCellType cellType){
		var port = new GameObject("Cell");
		port.AddComponent<RectTransform>();
		var cell = port.AddComponent<CircuitCell>().SetCellType(cellType);
		cellType.SetParent(cell);
		var img = port.AddComponent<Image>();
		img.color = Color.white;
		port.SetParent(container);
	}
	
	public void ChangeCellType(int index, CircuitCellType cellType){
		Transform child = displayCellGrid.transform.GetChild(index);
		var cell = child.GetComponent<CircuitCell>();

		if (cell == null) {
			Debug.LogError($"O GameObject '{child.name}' no índice {index} não tem o componente CircuitCell.");
			return;
		}

		cell.SetCellType(cellType);
	}

	private void BuildEditorTab(GameObject container){
		var tab = new PPanel("Editor") {
			BackColor = Color.white
		};

		var editor = tab.AddTo(container);
		AddBorder(editor);
		editor.AddOrGet<LayoutElement>().preferredWidth = 300;
		editor.GetComponent<LayoutElement>().flexibleHeight = 1;

		var editorContent = new PPanel("EditorContent") {
			Margin = new RectOffset(10, 10, 10, 10),
		};
		var editorContentObj = editorContent.AddTo(editor);
		AddBorder(editorContentObj);
		editorContentObj.AddOrGet<LayoutElement>().flexibleWidth = 1;
		editorContentObj.AddOrGet<LayoutElement>().flexibleHeight = 1;
		this.editorContent = editorContentObj;

		var infoPanel = new PPanel("EditorInfo");
		infoPanel.SetKleiPinkColor();
		var infoPanelObj = infoPanel.AddTo(editor);
		AddBorder(infoPanelObj);
		infoPanelObj.AddOrGet<LayoutElement>().flexibleWidth = 1;
		infoPanelObj.AddOrGet<LayoutElement>().preferredHeight = 200;
	}

	private void BuildFooter(GameObject container){
		var titleBar = new PPanel("Footer") {
			Spacing = 15,
			Direction = PanelDirection.Horizontal,
			Margin = new RectOffset(10, 10, 10, 10)
		};

		var textStyle = PUITuning.Fonts.UILightStyle;
		textStyle.fontSize = 15;

		var saveButton = new PButton() {
			Text = "Save",
			TextStyle = textStyle,
			OnClick = (go) => { SaveButtonClicked(); },
			Margin = new RectOffset(10, 10, 10, 10)
		};

		var cancelButton = new PButton() {
			Text = "Cancel",
			TextStyle = textStyle,
			OnClick = (go) => { CancelButtonClicked(); },
			Margin = new RectOffset(10, 10, 10, 10)
		};

		titleBar.AddChild(saveButton);
		titleBar.AddChild(cancelButton);

		var obj = titleBar.AddTo(container);
		AddBorder(obj);

		var layout = obj.AddOrGet<LayoutElement>();
		layout.flexibleWidth = 1;
	}

	private static void AddBorder(GameObject go){
		var outline = go.AddComponent<Outline>();
		outline.effectColor = Color.black;
		outline.effectDistance = new Vector2(1, 1);
	}

	public void OnCellClicked(CircuitCellType cellType){
		var content = cellType.BuildEditorContent();
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

	private void SaveButtonClicked(){
		Debug.Log("Saving state");
		Deactivate();
	}

	private void CancelButtonClicked(){
		Debug.Log("Canceling state");
		Deactivate();
	}
}
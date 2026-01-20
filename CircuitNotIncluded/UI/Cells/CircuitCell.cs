using PeterHan.PLib.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CircuitNotIncluded.UI.Cells;

/**
 * Class that represents a cell on edit circuit screen.
 * Handles the click events and the visual representation of the cell.
 */
public class CircuitCell : MonoBehaviour, IPointerClickHandler {
	private CircuitCellState? currentState;
	public CellOffset Offset { get; private set; }
	
	private Outline outline = null!;
	public static CircuitCell? Selected;
	private Image image = null!;
	
	public static readonly TextStyleSetting LabelStyle = PUITuning.Fonts.TextDarkStyle;
	public static readonly TextStyleSetting TitleStyle = PUITuning.Fonts.TextDarkStyle.DeriveStyle(16);
	
	public CircuitCell Init(CellOffset offset){
		Offset = offset;
		return this;
	}
	
	private void Start(){
		outline = gameObject.AddComponent<Outline>();
		outline.effectColor = Color.blue;
		outline.effectDistance = new Vector2(2, 2);
		outline.enabled = false;
	}

	public void OnPointerClick(PointerEventData eventData){
		Selected?.Deselect();
		Select();
		CircuitScreenManager.Instance.BuildSideScreen(currentState!);
	}

	private void Select(){
		Selected = this;
		outline.enabled = true;
	}

	private void Deselect(){
		outline.enabled = false;
	}

	public CircuitCellState GetCellType(){
		return currentState!;
	}

	public CircuitCell TransitionTo(CircuitCellState newState){
		currentState?.OnExit();
		
		newState.OnEnter(this);
		if (image == null) image = gameObject.AddOrGet<Image>();
		newState.UpdateImage(image);

		if(Selected != null){
			CircuitScreenManager.Instance.BuildSideScreen(newState);
		}
		
		currentState = newState;
		return this;
	}
}
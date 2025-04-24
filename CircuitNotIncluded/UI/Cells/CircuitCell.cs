using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CircuitNotIncluded.UI.Cells;

/**
 * Class that represents a cell on edit circuit screen.
 * Handles the click events and the visual representation of the cell.
 */
public class CircuitCell : MonoBehaviour, IPointerClickHandler {
	private CircuitCellType type = null!;
	private Outline outline = null!;
	private static CircuitCell? Selected;
	
	private void Start(){
		outline = gameObject.AddComponent<Outline>();
		outline.effectColor = Color.blue;
		outline.effectDistance = new Vector2(2, 2);
		outline.enabled = false;
	}

	public CircuitCell SetCellType(CircuitCellType type){
		this.type = type;
		return this;
	}

	public void OnPointerClick(PointerEventData eventData){
		Selected?.Deselect();
		Select();
		CircuitScreen.Instance.OnCellClicked(type);
	}

	private void Select(){
		Selected = this;
		outline.enabled = true;
	}

	private void Deselect(){
		outline.enabled = false;
	}
}
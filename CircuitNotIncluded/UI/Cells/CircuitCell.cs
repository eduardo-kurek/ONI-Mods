using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CircuitNotIncluded.UI.Cells;

/**
 * Class that represents a cell on edit circuit screen.
 * Handles the click events and the visual representation of the cell.
 */
public class CircuitCell : MonoBehaviour, IPointerClickHandler {
	private CircuitCellType type;
	
	public CircuitCell SetCellType(CircuitCellType type){
		this.type = type;
		return this;
	}

	public void OnPointerClick(PointerEventData eventData){
		CircuitScreen.Instance.OnCellClicked(type);
	}
}
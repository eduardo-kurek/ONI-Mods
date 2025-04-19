using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CircuitNotIncluded.UI;

/**
 * Class that represents a cell on edit circuit screen.
 * Handles the click events and the visual representation of the cell.
 */
public class CircuitCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	public void OnPointerEnter(PointerEventData eventData){
		var img = GetComponent<Image>();
		img.color = Color.blue;   
	}

	public void OnPointerExit(PointerEventData eventData){
		var img = GetComponent<Image>();
		img.color = Color.white;
	}
}
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
	private CircuitCellType type = null!;
	private Outline outline = null!;
	public static CircuitCell? Selected;
	private Image image = null!;
	
	public static readonly TextStyleSetting LabelStyle = PUITuning.Fonts.TextDarkStyle;
	public static readonly TextStyleSetting TitleStyle = PUITuning.Fonts.TextDarkStyle.DeriveStyle(16);
	
	private void Start(){
		outline = gameObject.AddComponent<Outline>();
		outline.effectColor = Color.blue;
		outline.effectDistance = new Vector2(2, 2);
		outline.enabled = false;
	}

	public CircuitCell SetCellType(CircuitCellType type){
		if (image == null) image = gameObject.AddOrGet<Image>();
		this.type = type;
		this.type.SetParent(this);
		this.type.UpdateImage(image);
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

	public CircuitCellType GetCellType(){
		return type;
	}

	public CellOffset GetOffset(){
		return type.GetOffset();
	}
}
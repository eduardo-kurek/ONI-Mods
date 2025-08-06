using CircuitNotIncluded.Utils;
using PeterHan.PLib.UI;
using UnityEngine;
using UnityEngine.UI;

namespace CircuitNotIncluded.UI.Cells;

public class PortCellData(
	string id = "Id",
	string description = "Description",
	string activeDescription = "Active Description",
	string inactiveDescription = "Inactive Description")
{
	public string id = id;
	public string description = description;
	public string activeDescription = activeDescription;
	public string inactiveDescription = inactiveDescription;
}

public abstract class PortCellType(PortCellData data, CellOffset offset) : CircuitCellType(offset) {
	public int GetIndex() => CircuitScreen.Instance.Circuit.ToLinearIndex(offset);
	public string GetId() => data.id.Trim();
	public CellOffset GetOffset() => offset;
	public int X() => offset.x;
	public int Y() => offset.y;

	protected abstract Sprite GetPortSprite();

	public override void UpdateImage(Image img){
		img.color = Color.white;
		img.sprite = GetPortSprite();
	}
	
	protected GameObject BuildIdField(GameObject container){
		return BuildTextField(container, "Id: ", data.id, 50, 
			(source, text) => {
				data.id = text;
			});
	}

	protected GameObject BuildDescriptionField(GameObject container){
		return BuildTextField(container, "Description: ", data.description, 255,
			(source, text) => {
				data.description = text;
			});
	}

	protected GameObject BuildActiveDescriptionField(GameObject container){
		return BuildTextField(container, "Active Description: ", data.activeDescription, 255,
			(source, text) => {
				data.activeDescription = text;
			});
	}

	protected GameObject BuildInactiveDescription(GameObject container){
		return BuildTextField(container, "Inactive Description: ", data.inactiveDescription, 255, 
			(source, text) => {
				data.inactiveDescription = text;
			});
	}
	
	protected virtual GameObject BuildDeleteButton(GameObject container){
		return new PButton("DeleteButton")
			.Text("Delete Port")
			.Margin(10)
			.SetOnClick((go) => { Delete(); })
			.AddTo(container);
	}

	protected virtual void Delete(){
		EmptyCellType type = new(offset);
		parent.SetCellType(type);
		parent.OnPointerClick(null!);
	}
}
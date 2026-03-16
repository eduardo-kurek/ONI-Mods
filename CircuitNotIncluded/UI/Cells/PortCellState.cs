using CircuitNotIncluded.Structs;
using CircuitNotIncluded.Utils;
using PeterHan.PLib.UI;
using UnityEngine;
using UnityEngine.UI;

namespace CircuitNotIncluded.UI.Cells;

public abstract class PortCellState(PortInfo info) : CircuitCellState {
	public string Id => info.Id.Trim();
	
	protected abstract Sprite PortSprite { get; }

	public override void UpdateCellImage(Image img){
		img.color = Color.white;
		img.sprite = PortSprite;
	}
	
	protected GameObject BuildIdField(GameObject container){
		return BuildTextField(container, "Id: ", info.Id, 50, 
			(source, text) => {
				info.Id = text;
			});
	}

	protected GameObject BuildDescriptionField(GameObject container){
		return BuildTextField(container, "Description: ", info.Description, 255,
			(source, text) => {
				info.Description = text;
			});
	}

	protected GameObject BuildActiveDescriptionField(GameObject container){
		return BuildTextField(container, "Active Description: ", info.ActiveDescription, 255,
			(source, text) => {
				info.ActiveDescription = text;
			});
	}

	protected GameObject BuildInactiveDescription(GameObject container){
		return BuildTextField(container, "Inactive Description: ", info.InactiveDescription, 255, 
			(source, text) => {
				info.InactiveDescription = text;
			});
	}
	
	protected GameObject BuildDeleteButton(GameObject container){
		GameObject deletePanel = new PPanel("DeletePanel")
			.Direction(PanelDirection.Horizontal)
			.Alignment(TextAnchor.MiddleCenter)
			.Margin(20, 0, 0, 0)
			.AddTo(container);
		
		return new PButton("DeleteButton")
			.Text("Delete Port")
			.Margin(10)
			.SetOnClick((go) => { Delete(); })
			.AddTo(deletePanel);
	}

	protected void Delete(){
		EmptyCellState state = new();
		Owner.TransitionTo(state);
	}
}
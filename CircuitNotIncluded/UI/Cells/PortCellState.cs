using CircuitNotIncluded.Core;
using CircuitNotIncluded.Utils;
using PeterHan.PLib.UI;
using UnityEngine;
using UnityEngine.UI;

namespace CircuitNotIncluded.UI.Cells;

public interface IHasId {
	string GetId();
}

public abstract class PortCellState : CircuitCellState, IHasId {
	protected abstract Sprite PortSprite { get; }

	public override void UpdateCellImage(Image img){
		img.color = Color.white;
		img.sprite = PortSprite;
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

	public void Delete(){
		EmptyCellState state = new();
		Owner.TransitionTo(state);
	}

	public abstract string GetId();
}
using CircuitNotIncluded.Core.DTO;
using CircuitNotIncluded.UI.Builders;
using CircuitNotIncluded.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace CircuitNotIncluded.UI.Cells;

public abstract class PortCellState : CircuitCellState {
	protected abstract Sprite PortSprite { get; }

	public abstract PortDTO CreateDTO();
	protected abstract void BuildPortContent(GameObject parent);
	
	public override GameObject BuildEditorContent(){
		GameObject panel = BuildContainer();
		BuildPortContent(panel);
		FieldBuilder.BuildDeleteButton(panel, (go) => Delete());
		return panel;
	}
	
	public void Delete(){
		EmptyCellState state = new();
		Owner.TransitionTo(state);
	}
	
	public override void UpdateCellImage(Image img){
		img.color = Color.white;
		img.sprite = PortSprite;
	}
	
	public override void OnEnter(CircuitCell owner){
		base.OnEnter(owner);
		CircuitScreenManager.Instance.OnPortCellCreated(this);
	}

	public override void OnExit(){
		CircuitScreenManager.Instance.OnPortCellDeleted(this);
	}
}
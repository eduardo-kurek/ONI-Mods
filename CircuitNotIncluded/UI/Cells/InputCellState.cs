using CircuitNotIncluded.Structs;
using UnityEngine;

namespace CircuitNotIncluded.UI.Cells;

public class InputCellState(PortInfo info) : PortCellState {
	private readonly PortInfo Info = info;
	protected override string CellTitle => "Input Port";
	protected override Sprite PortSprite => Assets.instance.logicModeUIData.inputSprite;
	public override string GetId() => Info.Id.Trim();
	
	public override bool Equals(object other){
		if(other is InputCellState o){
			return Info.Equals(o.Info);
		}
		return false;
	}

	public override int GetHashCode() => Info.GetHashCode();

	public InputCellState Clone() => new(info with {});

	public override GameObject BuildEditorContent(){
		GameObject panel = BuildContainer();
		BuildIdField(panel, "Id :", info);
		BuildDescriptionField(panel,"Description :", info);
		BuildActiveDescriptionField(panel, info);
		BuildInactiveDescription(panel, info);
		BuildDeleteButton(panel);
		return panel;
	}
	
	public InputPort ToPort(){
		return InputPort.Create(Owner.Offset, Info);
	}

	public static InputCellState Create(InputPort port){
		return new InputCellState(port.GetInfo());
	}

	public static InputCellState Create(){
		PortInfo info = PortInfo.Default();
		return new InputCellState(info);
	}

	public override void OnEnter(CircuitCell owner){
		base.OnEnter(owner);
		CircuitScreenManager.Instance.OnInputCellCreated(this);
	}

	public override void OnExit(){
		CircuitScreenManager.Instance.OnInputCellDeleted(this);
	}
}
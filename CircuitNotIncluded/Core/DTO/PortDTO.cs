using CircuitNotIncluded.Core.Model;
using CircuitNotIncluded.Interfaces;
using CircuitNotIncluded.UI.Cells;
using KSerialization;
using Newtonsoft.Json.Linq;

namespace CircuitNotIncluded.Core.DTO;

public enum PortCategory {
	Input,
	Output,
}

public abstract record PortDTO (
	[property: Serialize] CellOffset Offset
) : IBlueprintSerializable, IHover {
	public abstract PortCategory Category { get; }
	public abstract string GetDisplayText();
	
	protected static CellOffset ReadOffset(JObject json){
		return json["Offset"]?.ToObject<CellOffset>() ?? default; 
	}

	public virtual JObject ToJson() => new() {
		{ "Offset", JObject.FromObject(Offset) }
	};
	
	public abstract void OnHover(string circuitName, HoverTextDrawer drawer, SelectToolHoverTextCard cfg);
	public abstract IModel CreateModel(CircuitModel parent, OffsetResolver resolver);
	public abstract CircuitCellState CreateCellState();
}
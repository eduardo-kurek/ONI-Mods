using CircuitNotIncluded.Core.Model;
using CircuitNotIncluded.Interfaces;
using KSerialization;
using Newtonsoft.Json.Linq;

namespace CircuitNotIncluded.Core.DTO;

public abstract record PortDTO (
	[property: Serialize] CellOffset Offset
) : IBlueprintSerializable, IHover {
	
	protected static CellOffset ReadOffset(JObject json){
		return json["Offset"]?.ToObject<CellOffset>() ?? default; 
	}

	public virtual JObject ToJson() => new() {
		{ "Offset", JObject.FromObject(Offset) }
	};
	
	public abstract void OnHover(string circuitName, HoverTextDrawer drawer, SelectToolHoverTextCard cfg);
	public abstract IModel CreateModel(CircuitModel parent, OffsetResolver resolver);
}
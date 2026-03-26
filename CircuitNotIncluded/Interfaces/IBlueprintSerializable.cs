using Newtonsoft.Json.Linq;

namespace CircuitNotIncluded.Interfaces;

public interface IBlueprintSerializable {
	JObject ToJson();
}
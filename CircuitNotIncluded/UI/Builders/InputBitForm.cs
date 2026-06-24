using CircuitNotIncluded.Structs.Ports;
using CircuitNotIncluded.Utils;
using UnityEngine;

namespace CircuitNotIncluded.UI.Builders;

public class InputBitForm(InputBit inputBit) {
	public string id = inputBit.Id;
	private string description = inputBit.Description;
	
	public void Build(GameObject container){
		FieldBuilder.BuildIdField(container, id, (_, text) => { id = text; });
		FieldBuilder.BuildDescriptionField(container, description, (_, text) => { description = text; });
	}

	public InputBit GetValue() => new(id, description);
}
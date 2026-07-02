using CircuitNotIncluded.Core.DTO;
using CircuitNotIncluded.Utils;
using UnityEngine;

namespace CircuitNotIncluded.UI.Builders;

public class InputBitForm(InputBitDTO inputBitDto) {
	public string id = inputBitDto.Id;
	private string description = inputBitDto.Description;
	
	public void Build(GameObject container){
		FieldBuilder.BuildIdField(container, id, (_, text) => { id = text; });
		FieldBuilder.BuildDescriptionField(container, description, (_, text) => { description = text; });
	}

	public InputBitDTO GetValue() => new(id.Trim(), description.Trim());
}
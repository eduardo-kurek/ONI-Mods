using CircuitNotIncluded.Core.DTO;
using CircuitNotIncluded.Utils;
using UnityEngine;

namespace CircuitNotIncluded.UI.Builders;

public class OutputBitForm(OutputBitDTO outputBitDto) {
	public string label = outputBitDto.Label;
	private string description = outputBitDto.Description;
	public string expression = outputBitDto.Expression;
	
	public void Build(GameObject container){
		FieldBuilder.BuildLabelField(container, label, (_, text) => { label = text; });
		FieldBuilder.BuildDescriptionField(container, description, (_, text) => { description = text; });
		FieldBuilder.BuildExpressionField(container, expression, (_, text) => { expression = text; });
	}

	public OutputBitDTO GetValue() => new(label.Trim(), description.Trim(), expression);
}
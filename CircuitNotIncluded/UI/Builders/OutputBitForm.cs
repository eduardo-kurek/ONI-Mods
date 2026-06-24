using CircuitNotIncluded.Core.Structs;
using CircuitNotIncluded.Utils;
using UnityEngine;

namespace CircuitNotIncluded.UI.Builders;

public class OutputBitForm(OutputBit outputBit) {
	public string label = outputBit.Label;
	private string description = outputBit.Description;
	public string expression = outputBit.Expression;
	
	public void Build(GameObject container){
		FieldBuilder.BuildLabelField(container, label, (_, text) => { label = text; });
		FieldBuilder.BuildDescriptionField(container, description, (_, text) => { description = text; });
		FieldBuilder.BuildExpressionField(container, expression, (_, text) => { expression = text; });
	}

	public OutputBit GetValue() => new(label, description, expression);
}
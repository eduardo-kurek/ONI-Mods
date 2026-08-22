using CircuitNotIncluded.Core.DTO;
using CircuitNotIncluded.Core.Runtime;
using CircuitNotIncluded.Core.Validators;
using CircuitNotIncluded.Grammar.Visitors.Expression;
using CircuitNotIncluded.Interfaces;
using FluentValidation.Results;

namespace CircuitNotIncluded.Core.Model;

public class RibbonInputModel : PortModel {
	
	public InputBitModel Bit1 { get; }
	public InputBitModel Bit2 { get; }
	public InputBitModel Bit3 { get; }
	public InputBitModel Bit4 { get; }

	public RibbonInputModel(RibbonInputDTO dto, CircuitModel circuit, OffsetResolver resolver) 
		: base(dto, circuit, resolver){
		Bit1 = new InputBitModel(dto.Bit1, this, 1);
		Bit2 = new InputBitModel(dto.Bit2, this, 2);
		Bit3 = new InputBitModel(dto.Bit3, this, 3);
		Bit4 = new InputBitModel(dto.Bit4, this, 4);
	}
	
	public override ValidationPriority ValidationPriority => ValidationPriority.First;

	public override IRuntime CreateRuntime(SymbolTable symbolTable, Dictionary<string, ExpressionState> _)
		=> new RibbonInputRuntime(symbolTable, Bit1.Id, Bit2.Id, Bit3.Id, Bit4.Id, Index);

	public override ValidationResult Validate(ValidationData data){
		var validator = new RibbonInputValidator(data.declaredInputs);
		return validator.Validate(this);
	}

}
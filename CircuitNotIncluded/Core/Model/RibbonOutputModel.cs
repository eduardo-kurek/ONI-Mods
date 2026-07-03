using CircuitNotIncluded.Core.DTO;
using CircuitNotIncluded.Core.Runtime;
using CircuitNotIncluded.Core.Validators;
using CircuitNotIncluded.Grammar;
using CircuitNotIncluded.Interfaces;
using FluentValidation.Results;

namespace CircuitNotIncluded.Core.Model;

public class RibbonOutputModel : PortModel {
	
	public OutputBitModel Bit1 { get; }
	public OutputBitModel Bit2 { get; }
	public OutputBitModel Bit3 { get; }
	public OutputBitModel Bit4 { get; }

	public RibbonOutputModel(RibbonOutputDTO dto, CircuitModel circuit, OffsetResolver resolver) 
		: base(dto, circuit, resolver){
		Bit1 = new OutputBitModel(dto.Bit1, this, 1);
		Bit2 = new OutputBitModel(dto.Bit2, this, 2);
		Bit3 = new OutputBitModel(dto.Bit3, this, 3);
		Bit4 = new OutputBitModel(dto.Bit4, this, 4);
	}
	
	public override ValidationPriority ValidationPriority => ValidationPriority.Second;

	public override IRuntime CreateRuntime(SymbolTable symbolTable)
		=> new RibbonOutputRuntime(symbolTable, Bit1.Expression, Bit2.Expression, 
			Bit3.Expression, Bit4.Expression, Index);

	public override ValidationResult Validate(ValidationData data){
		var validator = new RibbonOutputValidator(data);
		return validator.Validate(this);
	}

}
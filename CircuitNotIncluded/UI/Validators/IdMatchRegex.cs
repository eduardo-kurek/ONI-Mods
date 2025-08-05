using System.Text.RegularExpressions;
using CircuitNotIncluded.UI.Cells;

namespace CircuitNotIncluded.UI.Validators;

public class IdMatchRegex(PortHandler? next = null) : PortHandler(next) {
	private const string RegexPattern = @"^[a-zA-Z_][a-zA-Z0-9_]*$";

	protected override bool ErrorOccurred(PortCellType cell, ValidationContext ctx)
		=> DoNotMatch(cell);
	
	private static bool DoNotMatch(PortCellType cell){
		return !Regex.IsMatch(cell.GetId(), RegexPattern);
	}
	
	protected override string GetErrorMessage(PortCellType cell, ValidationContext ctx){
		return "Port id must start with a letter or underscore " +
		       "and can only contain letters, numbers, and underscore.";
	}
}
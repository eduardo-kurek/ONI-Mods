namespace CircuitNotIncluded.Structs;

public record PortInfo(string Id, string Description, string ActiveDescription, string InactiveDescription) {
	public string Id = Id;
	public string Description = Description;
	public string ActiveDescription = ActiveDescription;
	public string InactiveDescription = InactiveDescription;

	public static PortInfo Default(){
		return new PortInfo(
			"Id",
			"Description", 
			"Active Description", 
			"Inactive Description"
		);
	}
}
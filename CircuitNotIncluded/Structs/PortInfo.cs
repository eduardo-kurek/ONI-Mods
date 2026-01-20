namespace CircuitNotIncluded.Structs;

public class PortInfo(
	string id,
	string description,
	string activeDescription,
	string inactiveDescription
){
	public string Id = id;
	public string Description = description;
	public string ActiveDescription = activeDescription;
	public string InactiveDescription = inactiveDescription;

	public static PortInfo Default(){
		return new PortInfo(
			"Id",
			"Description", 
			"Active Description", 
			"Inactive Description"
		);
	}
}
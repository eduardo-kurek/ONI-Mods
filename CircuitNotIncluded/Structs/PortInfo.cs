namespace CircuitNotIncluded.Structs;

public class PortInfo(
	string id,
	CellOffset offset,
	string description,
	string activeDescription,
	string inactiveDescription
){
	public string Id = id;
	public CellOffset Offset = offset;
	public string Description = description;
	public string ActiveDescription = activeDescription;
	public string InactiveDescription = inactiveDescription;

	public static PortInfo Default(CellOffset offset){
		return new PortInfo(
			"Id", 
			offset, 
			"Description", 
			"Active Description", 
			"Inactive Description"
		);
	}
}
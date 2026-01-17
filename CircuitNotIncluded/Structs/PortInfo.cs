namespace CircuitNotIncluded.Structs;

public class PortInfo(
	string id,
	CellOffset offset,
	string description,
	string activeDescription,
	string inactiveDescription
){
	public string Id { get; } = id;
	public CellOffset Offset { get; } = offset;
	public string Description { get; } = description;
	public string ActiveDescription { get; } = activeDescription;
	public string InactiveDescription { get; } = inactiveDescription;
}
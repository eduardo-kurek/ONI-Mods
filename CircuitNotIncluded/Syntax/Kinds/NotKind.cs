namespace CircuitNotIncluded.Syntax.Kinds;

public class NotKind : IUnaryKind {
	private static readonly Lazy<NotKind> _instance = new(() => new NotKind());
	public static NotKind Instance => _instance.Value;
	private NotKind(){ }
	
	public int Evaluate(int value){
		return ~value;
	}
}
namespace CircuitNotIncluded.Syntax.Kinds;

public class OrKind : IBinaryKind {
	private static readonly Lazy<OrKind> instance = new(() => new OrKind());
	public static OrKind Instance => instance.Value;
	private OrKind(){ }
	
	public int Evaluate(int left, int right){
		return left | right;
	}
}
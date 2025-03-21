namespace CircuitNotIncluded.Syntax.Kinds;

public class AndKind : IBinaryKind {
	private static readonly Lazy<AndKind> instance = new(() => new AndKind());
	public static AndKind Instance => instance.Value;
	private AndKind(){ }
	
	public int Evaluate(int left, int right){
		return left & right;
	}
}
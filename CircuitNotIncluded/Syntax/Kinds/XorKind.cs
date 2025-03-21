namespace CircuitNotIncluded.Syntax.Kinds;

public class XorKind : IBinaryKind {
	private static readonly Lazy<XorKind> _instance = new(() => new XorKind());
	public static XorKind Instance => _instance.Value;
	private XorKind(){ }
	
	public int Evaluate(int left, int right){
		return left ^ right;
	}
}
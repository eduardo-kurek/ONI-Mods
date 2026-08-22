using System.Text;
using KSerialization;

namespace CircuitNotIncluded.Grammar.Visitors.Expression;

[SerializationConfig(MemberSerialization.OptIn)]
public class ExpressionState {
	[Serialize] private TransitionDelayState[] delayStates = [];
	
	public ExpressionState(TransitionDelayState[] delayStates){
		this.delayStates = delayStates ?? [];
	}
	
	public int EvaluateDelayGate(int currentInput, int index){
		return delayStates[index].Evaluate(currentInput);
	}
}
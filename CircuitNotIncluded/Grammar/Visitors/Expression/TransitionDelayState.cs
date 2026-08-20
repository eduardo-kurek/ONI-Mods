using UnityEngine;

namespace CircuitNotIncluded.Grammar.Visitors.Expression;

public class TransitionDelayState(float delayAmount, int valueToAwait) : IStatefulGate {
	private bool wasPreviouslyTriggered;
	private int ticksRemaining;

	public int Evaluate(int currentInput){
		if (currentInput == valueToAwait){
			wasPreviouslyTriggered = true;
			ticksRemaining = 0;
		}
		else if (ticksRemaining <= 0){
			if (wasPreviouslyTriggered){
				ticksRemaining = Mathf.RoundToInt(delayAmount / LogicCircuitManager.ClockTickInterval);
			}
			wasPreviouslyTriggered = false;
		}

		int otherValue = 1 - valueToAwait;
		int result = (currentInput != valueToAwait && ticksRemaining <= 0) ? otherValue : valueToAwait;

		if (!wasPreviouslyTriggered && ticksRemaining > 0){
			ticksRemaining--;
		}

		return result;
	}
}
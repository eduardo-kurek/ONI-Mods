using UnityEngine;

namespace CircuitNotIncluded.Grammar.Visitors.Expression;

public class BufferState(float delayAmount) {
	private bool wasPreviouslyPositive;
	private int ticksRemaining;

	public int Evaluate(int currentInput){
		if (currentInput != 0){
			wasPreviouslyPositive = true;
			ticksRemaining = 0;
		}
		else if (ticksRemaining <= 0){
			if (wasPreviouslyPositive){
				ticksRemaining = Mathf.RoundToInt(delayAmount / LogicCircuitManager.ClockTickInterval);
			}
			wasPreviouslyPositive = false;
		}

		int result = (currentInput == 0 && ticksRemaining <= 0) ? 0 : 1;

		if (!wasPreviouslyPositive && ticksRemaining > 0){
			ticksRemaining--;
		}

		return result;
	}
}
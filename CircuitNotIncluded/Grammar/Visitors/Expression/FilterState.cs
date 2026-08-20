using UnityEngine;

namespace CircuitNotIncluded.Grammar.Visitors.Expression;

public class FilterState(float delayAmount) {
	private bool wasPreviouslyNegative; 
	private int ticksRemaining;

	public int Evaluate(int currentInput){
		if (currentInput == 0){
			wasPreviouslyNegative = true;
			ticksRemaining = 0;
		}
		
		else if (ticksRemaining <= 0){
			if (wasPreviouslyNegative){
				ticksRemaining = Mathf.RoundToInt(delayAmount / LogicCircuitManager.ClockTickInterval);
			}
			wasPreviouslyNegative = false;
		}
		
		int result = (currentInput != 0 && ticksRemaining <= 0) ? 1 : 0;
		
		if (!wasPreviouslyNegative && ticksRemaining > 0){
			ticksRemaining--;
		}
		
		return result;
	}
}
using Godot;
using System;
using System.Collections.Generic;

public partial class RandomAttackIdleState : AttackingState
{
	private RandomAttackingState[] attackingStates;
	private List<RandomAttackingState> validAttackingStates = new List<RandomAttackingState>();
	
	public RandomAttackIdleState(RandomAttackingState[] attackingStates) {
		this.attackingStates = attackingStates;
		foreach(RandomAttackingState state in attackingStates) {
			state.SetIdleState(this);
		}
	}

	public override IState NextState(double delta) {
		// Get valid attacks
		validAttackingStates = new List<RandomAttackingState>();
		foreach(RandomAttackingState state in attackingStates) {
			if(state.GetAttack().CanAttack())
				validAttackingStates.Add(state);
		}
		// Select random attack
		Random rand = new Random();
		int randIx = rand.Next(0, validAttackingStates.Count);
		return validAttackingStates[randIx];
	}

}

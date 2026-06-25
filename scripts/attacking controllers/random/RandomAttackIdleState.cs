using Godot;
using System;
using System.Collections.Generic;

public class RandomAttackIdleState : IState
{
	private AttackingState[] attackingStates;
	private List<AttackingState> validAttackingStates = new List<AttackingState>();

	public RandomAttackIdleState(AttackingState[] attackingStates) {
		this.attackingStates = attackingStates;
	}
	
	public virtual void Update(double delta) {}

	public IState NextState() {
		// Get valid attacks
		validAttackingStates = new List<AttackingState>();
		foreach(AttackingState state in attackingStates) {
			if(state.GetAttack().CanAttack())
				validAttackingStates.Add(state);
		}
		// Select random attack
		Random rand = new Random();
		int randIx = rand.Next(0, validAttackingStates.Count);
		return validAttackingStates[randIx];
	}

	public void OnEnter() {}
	public void OnLeave() {}
}

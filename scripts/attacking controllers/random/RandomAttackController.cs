using Godot;
using System;

public partial class RandomAttackController : FiniteStateController
{
	[Export] private AttackingState[] attackingStates;
	
	public override void _Ready() {
		base._Ready();
		this.state = new RandomAttackIdleState(attackingStates);
	}
}

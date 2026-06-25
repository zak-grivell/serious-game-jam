using Godot;
using System;

public partial class RandomAttackController : FiniteStateController
{
	[Export] private RandomAttackingState[] attackingStates;
	
	public override void _Ready() {
		base._Ready();
		this.state = new RandomAttackIdleState(attackingStates);
	}
}

using Godot;
using System;

public partial class RandomAttackingState : AttackingState
{
	protected AttackingState idleState;
	
	public void SetIdleState(AttackingState idleState) => this.idleState = idleState;

	public override IState NextState(double delta) => null;

	public override void OnEnter() {}
	public override void OnLeave() {}
}

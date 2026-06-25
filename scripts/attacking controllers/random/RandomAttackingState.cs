using Godot;
using System;

public partial class RandomAttackingState : AttackingState
{
	protected AttackingState idleState;
	protected double timer = 0;
	public void SetIdleState(AttackingState idleState) => this.idleState = idleState;

	public void IncrementTimer(double delta)
	{
		timer += delta;
	}
	public override IState NextState(double delta)
	{
		IncrementTimer(delta);
		return null;
	}

	public override void OnEnter() {}
	public override void OnLeave() {}
}

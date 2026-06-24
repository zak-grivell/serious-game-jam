using Godot;
using System;

public partial class WheelTurnerTestAttack : AttackingState
{
	public override void Update(double delta) {}

	public override IState NextState() => null;

	public override void OnEnter() {}
	public override void OnLeave() {}
	
	public override IAttack GetAttack() => null;
}

using Godot;
using System;

public partial class WheelTurnerTestAttack : RandomAttackingState
{
	[Export] private double attackCooldown;
	private double currentCooldown = 0;
	
	public override void _Ready() {
		base._Ready();
		attack = new DebugPrintAttack("Attacking!! >=)");
	}

	public override IState NextState(double delta) {
		if(currentCooldown >= attackCooldown) {
			currentCooldown = 0;
			return idleState;
		}else if(currentCooldown == 0 && GetAttack().CanAttack())
			GetAttack().Attack(delta);
		currentCooldown += delta;
		return this;
	}

	public override void OnEnter() {}
	public override void OnLeave() {}
}

using Godot;
using System;

public partial class AttackingState : Node, IState
{
	protected IAttack attack;
	
	public virtual IState NextState(double delta) => null;

	public virtual void OnEnter() {}
	public virtual void OnLeave() {}
	
	public IAttack GetAttack() => this.attack;
}

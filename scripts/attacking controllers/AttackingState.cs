using Godot;
using System;

public partial class AttackingState : Node, IState
{
	
	public virtual void Update(double delta) {}

	public virtual IState NextState() => null;

	public virtual void OnEnter() {}
	public virtual void OnLeave() {}
	
	public virtual IAttack GetAttack() => null;
}

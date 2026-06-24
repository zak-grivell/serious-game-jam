using Godot;
using System;

public partial class AttackingState : Node, IState
{
	
	public void Update(double delta) {}

	public IState NextState() => null;

	public void OnEnter() {}
	public void OnLeave() {}
	
	public IAttack GetAttack() => null;
}

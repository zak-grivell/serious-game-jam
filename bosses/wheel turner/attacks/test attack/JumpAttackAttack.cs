using Godot;
using System;

public partial class JumpAttackAttack : IAttack
{
	[Export] private CharacterBody2D AttackOwner;
	
	public JumpAttackAttack(CharacterBody2D AttackOwner) {
		this.AttackOwner = AttackOwner;
	}
	
	public void Attack(double delta) {
		GD.Print("Running attack!");
		AttackOwner.Velocity = new Vector2(0f, -10f);
	}
	
	public bool CanAttack() => true;
}

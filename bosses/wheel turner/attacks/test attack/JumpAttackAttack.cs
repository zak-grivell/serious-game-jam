using Godot;
using System;

public partial class JumpAttackAttack : IAttack
{
	[Export] private CharacterBody2D AttackOwner;
	private float strength;
	
	public JumpAttackAttack(CharacterBody2D AttackOwner, float strength) {
		this.AttackOwner = AttackOwner;
		this.strength = strength;
	}
	
	public void Attack(double delta) {
		GD.Print("Running attack!");
		AttackOwner.Velocity = new Vector2(0f, -strength);
	}
	
	public bool CanAttack() => true;
}

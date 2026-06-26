using Godot;
using System;

public partial class EntityLaunchProjectileAttack : ProjectileAttack
{
	[Export] private Node2D originNode;
	
	// Awful design but i can't be bothered to improve it
	public override void Attack(double delta) {
		SetOrigin(originNode.Position);
		base.Attack(delta);
	}
	
}

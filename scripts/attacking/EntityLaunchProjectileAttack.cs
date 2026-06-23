using Godot;
using System;

public partial class EntityLaunchProjectileAttack : ProjectileAttack
{
	[Export] private Node2D originNode;
	
	// Awful design but i can't be bothered to improve it
	public void Attack() {
		SetOrigin(originNode.Position);
		base.Attack();
	}
}

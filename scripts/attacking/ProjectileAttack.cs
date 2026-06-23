using Godot;
using System;

public partial class ProjectileAttack : Node, IAttack
{
	[Export] protected PackedScene projectile;
	protected Vector2 origin = Vector2.Zero;
	
	public void SetOrigin(Vector2 origin) => this.origin = origin;
	
	public virtual void Attack() {
		// Terrible practice =,(   vvvvv
		var proj = (Node2D) projectile.Instantiate();
		proj.Position = origin;
	}
}

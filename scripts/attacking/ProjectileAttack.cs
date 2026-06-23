using Godot;
using System;

public partial class ProjectileAttack : IAttack, Node
{
	[Export] protected PackedScene projectile;
	protected Vector2 origin = Vector2.Zero;
	
	public void SetOrigin(Vector2 origin) => this.origin = origin;
	
	public void Attack() {
		var proj = projectile.Instantiate();
		proj.Position = origin;
	}
}

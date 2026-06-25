using Godot;
using System;

public partial class GravityComp : Node
{
	[Export] private CharacterBody2D rb;
	[Export] private double gravity;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		rb.Velocity += new Vector2(0, (float) (delta*gravity));
	}
	
	public void SetGravity(double gravity) => this.gravity = gravity;
}

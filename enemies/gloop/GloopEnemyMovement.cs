using Godot;
using System;

public partial class GloopEnemyMovement : FiniteStateController
{
	[Export] private float pos1, pos2, precision, speed;
	[Export] private RigidBody2D rb;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		this.state = SimpleGoombaMovement.ToFirst(pos1, pos2, precision, speed, rb);
	}
}

using Godot;
using System;

public partial class WheelTurnerMoveset : FiniteStateController
{
	[Export] private Node root;
	[Export] private CharacterBody2D rb;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		this.state = new WheelTurnerIdleState();
	}
}

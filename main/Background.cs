using Godot;
using System;

public partial class Background : ColorRect
{
	private RigidBody2D Player;
	private Camera2D Camera;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Camera = GetNode<Camera2D>("../../Scene/CharacterBody2D/Camera2D");
		Size = Camera.GetViewportRect().Size;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{	
		//GD.Print("running");
	}
}

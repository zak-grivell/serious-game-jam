using Godot;
using System;

public partial class Background : ColorRect
{
	private RigidBody2D Player;
	private Camera2D Camera;

	[Export]
	private float DayProgressRate;
	private float DayProgress;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Player = GetNode<RigidBody2D>("../../Scene/CharacterBody2D");
		Camera = GetNode<Camera2D>("../../Scene/CharacterBody2D/Camera2D");
		Size = Camera.GetViewportRect().Size;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// having the background manage the day progress is probably an awful idea but i don't really care

		DayProgress += DayProgressRate * (float)delta;
		(Material as ShaderMaterial).SetShaderParameter("DayProgress", DayProgress);
	}
}

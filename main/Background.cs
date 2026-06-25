using Godot;
using NewGameProject;
using System;

public partial class Background : ColorRect
{
	private RigidBody2D Player;
	private Camera2D Camera;

	[Export]
	private float DayProgressRate;
	// day progress goes between 0 and two pi, 0 represents halfway through night
	private float DayProgress;
	private Sprite2D Sun;
	private Sprite2D Moon;
	[Export]
	private float OrbitHorizRadius = 600;
	[Export]
	private float OrbitVertRadius = 300;
	[Export]
	private float AngularOffset = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Player = GetNode<RigidBody2D>("../../Scene/CharacterBody2D");
		Camera = GetNode<Camera2D>("../../Scene/CharacterBody2D/Camera2D");
		Sun = GetNode<Sprite2D>("Sun");
		Moon = GetNode<Sprite2D>("Moon");

		Size = Camera.GetViewportRect().Size;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		DayProgress += DayProgressRate * (float)delta;
		// having the background manage the day progress is probably an awful idea but i don't really care
		float angle = DayProgress + AngularOffset;
		Sun.Position = OrbitCentre() + new Vector2(OrbitHorizRadius * Mathf.Cos(angle), OrbitVertRadius * Mathf.Sin(angle));
		Moon.Position = OrbitCentre() + new Vector2(OrbitHorizRadius * Mathf.Cos(angle + Mathf.Pi), OrbitVertRadius * Mathf.Sin(angle + Mathf.Pi));

		(Material as ShaderMaterial).SetShaderParameter("DayProgress", DayProgress);
	}

	public Vector2 OrbitCentre()
	{
		return Position + Size / 2f;
	}
}

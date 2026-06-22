using Godot;
using System;

public partial class LinearVectorMovement : Node
{
	[Export] private Node root;
	[Export] private CharacterBody2D rb;
	private Vector2 directionVector;
	
	public override void _Ready()
	{
		SetDirectionVector((Vector2) root.GetMeta("directionVector"));
	}
	
	public override void _Process(double delta) {
		rb.MoveAndSlide();
	}

	public void SetDirectionVector(Vector2 directionVector) {
		this.directionVector = directionVector;
		rb.Velocity = directionVector;
		GD.Print("Velocity: " + rb.Velocity);
	}
}

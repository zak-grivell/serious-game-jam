using Godot;
using System;

public partial class GloopEnemyMovement : FiniteStateController
{
    [Export] private Node root;
	[Export] private CharacterBody2D rb;
    private float pos1, pos2, speed;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		base._Ready();
		InitNewState(
			(float) root.GetMeta("pos1"),
			(float) root.GetMeta("pos2"),
			(float) root.GetMeta("speed")
        );
	}

	private void CreateState() => this.state = SimpleGoombaMovement.ToFirst(pos1, pos2, speed, rb);

    public void InitNewState(float pos1, float pos2, float speed) {
		this.pos1 = pos1;
		this.pos2 = pos2;
		this.speed = speed;
		CreateState();
	}
}

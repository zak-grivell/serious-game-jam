using Godot;
using System;

public partial class FiniteStateController : Node2D
{
	// If state is null then nothing will happen and the state can never be changed
	protected IState state;

	private Node2D player;

	// Override this to set the initial state
	public override void _Ready()
	{
		player = (Node2D)GetTree().GetFirstNodeInGroup("Player");	
		base._Ready();
	}

	public override void _Process(double delta)
	{
		if (player == null || GlobalPosition.DistanceTo(player.GlobalPosition) > 300) return;
		base._Process(delta);
		if (state != null)
		{
			IState nextState = state.NextState(delta);
			if (nextState != null && nextState != this.state) {
				this.state.OnLeave();
				nextState.OnEnter();
			}
			this.state = nextState;
		}
	}
}

using Godot;
using System;

public partial class FiniteStateController : Node
{
	// If state is null then nothing will happen and the state can never be changed
	protected IState state;

	private Node2D player;

	bool activated = false;

	// Override this to set the initial state
	public override void _Ready()
	{
		player = (Node2D)GetTree().GetFirstNodeInGroup("player");	
		base._Ready();
	}

	public override void _Process(double delta)
	{
		if (player != null || GetParent<Node2D>().GlobalPosition.DistanceTo(player.GlobalPosition) < 500) {
			activated = true;
		}

		if (!activated) {
			return;
		}
		
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

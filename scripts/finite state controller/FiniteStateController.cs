using Godot;
using System;

public partial class FiniteStateController : Node
{
	// If state is null then nothing will happen and the state can never be changed
	protected IState state;

	// Override this to set the initial state
	public override void _Ready()
	{
		base._Ready();
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
		if (state != null)
		{
			state.Update(delta);
			IState nextState = state.NextState();
			if (nextState != null && nextState != this.state) {
				this.state.OnLeave();
				nextState.OnEnter();
			}
			this.state = nextState;
		}
	}
}

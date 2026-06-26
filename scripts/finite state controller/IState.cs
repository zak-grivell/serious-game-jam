using Godot;
using System;

public interface IState
{

	// Returns the next state that the finite state controller should switch to
	// If no state change happens then just return this
	public IState NextState(double delta);

	public void OnEnter();
	public void OnLeave();
}

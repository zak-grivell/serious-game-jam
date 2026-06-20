using Godot;
using System;

public interface IState
{
	// Equivalent to _Process(delta)
	public void Update(double delta);

	// Returns the next state that the finite state controller should switch to
	// If no state change happens then just return this
	public IState NextState();

	public void OnEnter();
	public void OnLeave();
}

using Godot;
using System;
using System.Diagnostics;

public class SimpleGoombaMovementState : IState
{
	private CharacterBody2D rb;
	private float target, speed;
	private IState otherState;
	private bool rightwards;

	public SimpleGoombaMovementState(CharacterBody2D rb, float targetX, float speed, IState otherState) {
		this.rb = rb;
		this.target = targetX;
		this.speed = speed;
		this.otherState = otherState;
	}

	public SimpleGoombaMovementState(CharacterBody2D rb, float targetX, float speed) : this(rb, targetX, speed, null)
	{ }

	public void SetOtherState(IState otherState) => this.otherState = otherState;

	public IState NextState(double delta) {
		if ((rightwards && rb.Position.X >= target) || (!rightwards && rb.Position.X <= target)) return otherState;
		rb.Velocity = new Vector2((rightwards ? 1 : -1) * speed, rb.Velocity.Y);
		rb.MoveAndSlide();
		return this;
	}

	public void SetTarget(float target) {
		this.target = target;
		rightwards = target > rb.Position.X;
	}

	public void OnEnter() {
		SetTarget(this.target);
	}
	public void OnLeave() {}
}

using Godot;
using System;
using System.Diagnostics;

public class SimpleGoombaMovementState : IState
{
    private RigidBody2D rb;
    private float target, precision, speed;
    private IState otherState;

    public SimpleGoombaMovementState(RigidBody2D rb, float targetX, float precision, float speed, IState otherState) {
        this.rb = rb;
        this.target = targetX;
        this.precision = precision;
        this.speed = speed;
        this.otherState = otherState;
    }

    public SimpleGoombaMovementState(RigidBody2D rb, float targetX, float precision, float speed) : this(rb, targetX, precision, speed, null)
    { }

    public void SetOtherState(IState otherState) => this.otherState = otherState;

    public virtual void Update(double delta) {}

    public IState NextState() {
        if (Mathf.Abs(target - rb.Position.X) <= precision) return otherState;
        rb.LinearVelocity = new Vector2(((target > rb.Position.X) ? 1 : -1) * speed, rb.LinearVelocity.Y);
        GD.Print("Applying velocity: " + rb.LinearVelocity);
        return this;
    }

    public void OnEnter() {}
    public void OnLeave() {}
}

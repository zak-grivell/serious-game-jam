using Godot;
using System;

public partial class PlayerController : RigidBody2D
{
	private const float SPIN_SPEED = 10.0f;
	private const float SPIN_ACCEL = 5f;
	private const float MAX_SPIN = 1000.0f;
	private const float SPIN_FORCE = 100.0f;
	private const float JUMP_FORCE = -600.0f;
	private const float VERTICAL_BOOST_MULTIPLIER = 1f;

	private RayCast2D OnFloor;

	public override void _Ready() {
		OnFloor = GetNode<RayCast2D>("OnFloor");
	}

	public override void _PhysicsProcess(double delta)
	{
		float direction = Input.GetAxis("ui_left", "ui_right");

		OnFloor.Rotation = -Rotation;

		//AngularVelocity = direction * SPIN_SPEED;

		//LinearVelocity = LinearVelocity with
		//{
		//	X = LinearVelocity.X + direction * SPIN_FORCE
		//};

		// if the button is held, spin up that direction
		if (Input.IsActionPressed("ui_left") || Input.IsActionPressed("ui_right"))
		{
			AngularVelocity += direction * SPIN_ACCEL;
			AngularVelocity = Mathf.Clamp(AngularVelocity, -MAX_SPIN, MAX_SPIN);
			GD.Print("direction held");
		}

		if (Input.IsActionJustReleased("ui_left") || Input.IsActionJustReleased("ui_right"))
		{
			LinearVelocity = LinearVelocity with
			{
				X = LinearVelocity.X + direction * SPIN_FORCE,
				Y = -Mathf.Abs(AngularVelocity) * VERTICAL_BOOST_MULTIPLIER
			};

			GD.Print("direction released " + LinearVelocity.ToString());
		}

		if (Input.IsActionJustPressed("ui_accept") && OnFloor.IsColliding())
		{
			LinearVelocity = LinearVelocity with
			{
				X = LinearVelocity.X,
				Y = JUMP_FORCE
			};
		}
	}
}

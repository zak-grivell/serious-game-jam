using Godot;
using System;

public partial class PlayerController : RigidBody2D
{

	private const float SPIN_SPEED = 10.0f;
	private const float MAX_SPIN = 100.0f;
	private const float SPIN_LAUNCH_MULTIPLIER = 100.0f;
	private const float JUMP_FORCE = -600.0f;
	private const float VERTICAL_BOOST_MULTIPLIER = 14f;
	private const float SPIN_DECEL = 0.1f;
	private Sprite2D Sprite;
	private float SpinSpeed = 0;
	private RayCast2D OnFloor;
	private const double MAX_CHARGE_TIME = 2.0f;
	private double ChargeTimer = 0;
	private float SPIN_ACCEL = (float)(MAX_SPIN / MAX_CHARGE_TIME);
	public override void _Ready() {
		OnFloor = GetNode<RayCast2D>("OnFloor");
		Sprite = GetNode<Sprite2D>("Sprite2D");
	}

	public override void _PhysicsProcess(double delta)
	{
		float direction = Input.GetAxis("ui_left", "ui_right");

		OnFloor.Rotation = -Rotation;

		if (Input.IsActionJustPressed("ui_left") || Input.IsActionJustPressed("ui_right"))
		{
			ChargeTimer = 0;
		}

		// if released, launch
		if (Input.IsActionJustReleased("ui_left") || Input.IsActionJustReleased("ui_right"))
		{
			LinearVelocity = LinearVelocity with
			{
				X = LinearVelocity.X + SpinSpeed * SPIN_LAUNCH_MULTIPLIER,
				Y = -Mathf.Abs(SpinSpeed) * VERTICAL_BOOST_MULTIPLIER
			};

			GD.Print("direction released " + LinearVelocity.ToString());
			GD.Print("spin speed " + SpinSpeed);
		}
		
		// if the button is held, spin up that direction
		if (Input.IsActionPressed("ui_left") || Input.IsActionPressed("ui_right"))
		{
			if (ChargeTimer < MAX_CHARGE_TIME)
			{
				// uhhh??
				SpinSpeed += direction * SPIN_ACCEL * (float)delta;
				SpinSpeed = Mathf.Clamp(SpinSpeed, -MAX_SPIN, MAX_SPIN);
				ChargeTimer += delta;
				GD.Print("spinning up");
			}
			else
			{
				ChargeTimer = MAX_CHARGE_TIME;
			}

			GD.Print("direction held, spin speed " + SpinSpeed.ToString() + " charge timer " + ChargeTimer);
		}
		else
		{
			if (OnFloor.IsColliding())
			{
				SpinSpeed = Mathf.Lerp(SpinSpeed, 0, 0.07f);
				GD.Print("slowing down, spin speed is " + SpinSpeed.ToString());
			}
		}


		if (Input.IsActionJustPressed("ui_accept") && OnFloor.IsColliding())
		{
			LinearVelocity = LinearVelocity with
			{
				X = LinearVelocity.X,
				Y = JUMP_FORCE
			};
		}

		Sprite.Rotation += SpinSpeed;
	}
}

using Godot;
using NewGameProject.main;
using System;

public partial class PlayerController : RigidBody2D
{
	// TO-DO: make the player slow when charging up
	private const float MAX_SPIN = 0.4f;
	private const float LAUNCH_MAX_SPEED = 1000.0f;
	private const float JUMP_FORCE = -600.0f;
	private const float VERTICAL_BOOST_MULTIPLIER = 1000f;
	private Sprite2D Sprite;
	private float SpinSpeed = 0;
	private RayCast2D OnFloor;
	private const double MAX_CHARGE_TIME = 2.0f;
	private double ChargeTimer = 0;
	private float SPIN_ACCEL = (float)(MAX_SPIN / MAX_CHARGE_TIME);
	private double ChargeUpInterpolant;
	private float HeldDirection;
	private bool InAir;

	public override void _Ready() {
		OnFloor = GetNode<RayCast2D>("OnFloor");
	}

	public override void _PhysicsProcess(double delta)
	{
		float direction = Input.GetAxis("ui_left", "ui_right");

		OnFloor.Rotation = -Rotation;

		// if we FELL to the ground, not as we leave it
		if (OnFloor.IsColliding() && InAir && LinearVelocity.Y > 0)
		{
			InAir = false;
			ChargeUpInterpolant = 0;
			GD.Print("touched ground");
		}

		if (!InAir)
		{

			if (Input.IsActionJustPressed("ui_left") || Input.IsActionJustPressed("ui_right"))
			{
				ChargeTimer = 0;
				HeldDirection = direction;
			}

			// if released, launch
			if (Input.IsActionJustReleased("ui_left") || Input.IsActionJustReleased("ui_right"))
			{
				LinearVelocity = LinearVelocity with
				{
					X = LinearVelocity.X + HeldDirection * (float)ChargeUpInterpolant * LAUNCH_MAX_SPEED,
					Y = -(float)ChargeUpInterpolant * VERTICAL_BOOST_MULTIPLIER
				};

				GD.Print("direction released " + LinearVelocity.ToString());
				GD.Print("charge proportion " + (float)ChargeUpInterpolant);

				InAir = true;
			}

			// if the button is held, spin up that direction
			if (Input.IsActionPressed("ui_left") || Input.IsActionPressed("ui_right"))
			{
				if (ChargeTimer < MAX_CHARGE_TIME)
				{
					// uhhh??
					//SpinSpeed += direction * SPIN_ACCEL * (float)delta;
					//SpinSpeed = Mathf.Clamp(SpinSpeed, -MAX_SPIN, MAX_SPIN);
					LinearVelocity = Maths.Lerp(LinearVelocity, Vector2.Zero, (float)ChargeUpInterpolant);
					ChargeTimer += delta;
					ChargeUpInterpolant = ChargeTimer / MAX_CHARGE_TIME;
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
					//GD.Print("slowing down, spin speed is " + SpinSpeed.ToString());
				}
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

		SpinSpeed = (float)ChargeUpInterpolant * MAX_SPIN;

		if (!InAir)
		{
			Sprite.Rotation += HeldDirection * SpinSpeed;
		}

		Sprite.Modulate = InAir ? new Color(0, 0, 1) : new Color(0, 1, 0);
	}
}

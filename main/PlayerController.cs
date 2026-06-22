using Godot;
using System;

public partial class PlayerController : RigidBody2D
{
	// TO-DO: make the player slow when charging up
	private const float MAX_SPIN = 0.4f;
	private const float LAUNCH_MAX_SPEED = 1000.0f;
	private const float JUMP_FORCE = -600.0f;
	private const float VERTICAL_BOOST_MULTIPLIER = 5000f;

	private Sprite2D Sprite;
	private AnimationPlayer Animation;
	private RayCast2D floorRaycast;
	private const float CHARGE_RATE = 20.0f;
	private const float DECHARGE_RATE = 200.0f;
	private double NormalisedCharge = 0;
	private const int SlowestFPS = 6;
	private const int FastestFPS = 24;

	public override void _Ready()
	{
		floorRaycast = GetNode<RayCast2D>("OnFloor");
		Sprite = GetNode<Sprite2D>("Sprite2D");
		Animation.SpeedScale = 1f;
	}

	public override void _PhysicsProcess(double delta)
	{
		int direction = MathF.Sign(Input.GetAxis("ui_left", "ui_right"));

		if (direction != 0)
		{
			Sprite.FlipH = direction == -1;
		}

		Boolean isOnFloor = floorRaycast.IsColliding();

		floorRaycast.Rotation = -Rotation;

		if (isOnFloor)
		{

			if (direction == 0)
			{
				LinearVelocity = LinearVelocity with
				{
					X = LinearVelocity.X + (float)NormalisedCharge * LAUNCH_MAX_SPEED,
					Y = -MathF.Abs((float)NormalisedCharge) * VERTICAL_BOOST_MULTIPLIER
				};

				if (NormalisedCharge != 0) {
					AngularVelocity += (float)NormalisedCharge * 5;
				} else {
					AngularVelocity -= 0.5f * AngularVelocity * Rotation;
				}
				
				NormalisedCharge = 0;
			}
			else
			{
				LinearVelocity = LinearVelocity.Lerp(
					Vector2.Zero,
					MathF.Abs((float)NormalisedCharge)
				);

				NormalisedCharge = Mathf.Clamp(Mathf.Lerp(NormalisedCharge, direction, delta * CHARGE_RATE), -1, 1);
				Rotation = Mathf.LerpAngle(Rotation, -0.5f * direction, 0.1f);
			}

			if (Input.IsActionJustPressed("ui_accept"))
			{
				LinearVelocity = LinearVelocity with
				{
					X = LinearVelocity.X,
					Y = JUMP_FORCE
				};
			}

			Animation.SpeedScale = (float)Mathf.Lerp(1f, FastestFPS / SlowestFPS, Mathf.Abs(NormalisedCharge));
			GD.Print(Animation.SpeedScale.ToString());

		}


	}
}

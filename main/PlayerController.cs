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
	private AnimationPlayer an;
	
	private RayCast2D floorRaycast;
	private const float CHARGE_RATE = 20.0f;
	private const float DECHARGE_RATE = 200.0f;
	private double NormalisedCharge = 0;
	private const int SlowestFPS = 6;
	private const int FastestFPS = 24;
	private ProgressBar LaunchBar;
	// line above is for launch bar

	public override void _Ready()
	{
		floorRaycast = GetNode<RayCast2D>("OnFloor");
		Sprite = GetNode<Sprite2D>("Sprite2D");
		// Sprite.SpriteFrames.SetAnimationSpeed("default", SlowestFPS);

		an = GetNode<AnimationPlayer>("AnimationPlayer");
		
		LaunchBar = GetNode<ProgressBar>("LaunchBar");
		LaunchBar.MaxValue = 1.0;
		LaunchBar.Value = 0.0;
		LaunchBar.Visible = false;
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
				
				LaunchBar.Value = 0;
				LaunchBar.Visible = false;
			}
			else
			{
				LinearVelocity = LinearVelocity with {
					X = Mathf.Lerp(LinearVelocity .X, 0, MathF.Abs((float)NormalisedCharge))
				};
			
				NormalisedCharge = Mathf.Clamp(Mathf.Lerp(NormalisedCharge, direction, delta * CHARGE_RATE), -1, 1);
				Rotation = Mathf.LerpAngle(Rotation, -0.5f * direction, 0.1f);
				LaunchBar.Visible = true;
				LaunchBar.Value = MathF.Abs((float)NormalisedCharge);
			}

			if (Input.IsActionJustPressed("ui_accept"))
			{
				LinearVelocity = LinearVelocity with
				{
					X = LinearVelocity.X,
					Y = JUMP_FORCE
				};
			}

			an.SpeedScale = (float)NormalisedCharge * 10;

			//idk if i should be doing this every frame but whatever
			(Sprite.Material as ShaderMaterial).SetShaderParameter("CurrentFrame", Sprite.Frame);
			(Sprite.Material as ShaderMaterial).SetShaderParameter("FrameCount", Sprite.Hframes);

			GD.Print((Sprite.Material as ShaderMaterial).GetShaderParameter("CurrentFrame"));
			GD.Print((Sprite.Material as ShaderMaterial).GetShaderParameter("FrameCount"));
			
			// Sprite.SpriteFrames.SetAnimationSpeed("default", Mathf.Lerp(SlowestFPS, FastestFPS, Mathf.Abs(NormalisedCharge)));
		}


	}
}

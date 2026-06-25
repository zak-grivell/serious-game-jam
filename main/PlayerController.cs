using Godot;
using NewGameProject;
using System;
using System.Transactions;

public partial class PlayerController : RigidBody2D
{
	// TO-DO: make the player slow when charging up
	private const float MAX_SPIN = 0.4f;
	private const float LAUNCH_MAX_SPEED = 1000.0f;
	private const float JUMP_FORCE = -600.0f;
	private const float VERTICAL_BOOST_MULTIPLIER = 500f;

	//private Sprite2D Sprite;
	private AnimatedSprite2D Sprite;
	private AnimationPlayer an;

	private RayCast2D floorRaycast;
	private const float CHARGE_RATE = 5.0f;
	private const float DECHARGE_RATE = 200.0f;
	private double NormalisedCharge = 0;
	private const int SlowestFPS = 6;
	private const int FastestFPS = 24;
	private ProgressBar LaunchBar;
	private bool CanDamage => Mathf.Abs((float)NormalisedCharge) > 0.99;
	private const double FLAME_FADE_IN_TIME = 0.8;
	private double FlameFadeInTimer = 0;
	private bool WasOnFloorLastFrame;
	private int FlightDirection;

	private bool justBounced = false;
	private bool InDamagingFlight;
	public override void _Ready()
	{
		floorRaycast = GetNode<RayCast2D>("OnFloor");
		Sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		Sprite.SpriteFrames.SetAnimationSpeed("default", SlowestFPS);

		LaunchBar = GetNode<ProgressBar>("LaunchBar");
		LaunchBar.MaxValue = 1.0;
		LaunchBar.Value = 0.0;
		LaunchBar.Visible = false;
		FlameFadeInTimer = 0;
	}

	public override void _PhysicsProcess(double delta)
	{
		int direction = MathF.Sign(Input.GetAxis("ui_left", "ui_right"));
		bool isPressed = Input.IsActionPressed("ui_left") || Input.IsActionPressed("ui_right");

		//if (direction != 0)
		//{
		//	Sprite.FlipH = direction == -1;
		//}

		Boolean isOnFloor = floorRaycast.IsColliding();

		if (isOnFloor && !WasOnFloorLastFrame)
		{
			Land();
		}

		if (isPressed && isOnFloor)
		{
			FlightDirection = direction;
			Sprite.FlipH = direction == -1;
		}

		floorRaycast.Rotation = -Rotation;

		if (!isOnFloor)
		{
			//Sprite.Rotation = MathUtils.VectorToAngle(LinearVelocity) + Mathf.Pi;
		}

		bool isLaunch = !isPressed && NormalisedCharge != 0;
		bool isCharging = isPressed && isOnFloor;


		if (isLaunch && isOnFloor)
		{
			LinearVelocity = LinearVelocity with
			{
				X = LinearVelocity.X + (float)NormalisedCharge * LAUNCH_MAX_SPEED,
				Y = HeightFromLaunch(NormalisedCharge)
			};

			if (CanDamage)
			{
				InDamagingFlight = true;
			}

			// AngularVelocity += (float)NormalisedCharge * 5; the spinnies

			Rotation = MathUtils.VectorToAngle(LinearVelocity) - FlightDirection * MathF.PI / 2;
			//NormalisedCharge = 0;
			LaunchBar.Value = 0;
			LaunchBar.Visible = false;
		}
		else if (isCharging)
		{
			LinearVelocity = LinearVelocity with
			{
				X = Mathf.Lerp(LinearVelocity.X, 0, MathF.Abs((float)NormalisedCharge))
			};

			NormalisedCharge = Mathf.Clamp(Mathf.Lerp(NormalisedCharge, direction, delta * CHARGE_RATE), -1, 1);

			Rotation = Mathf.LerpAngle(Rotation, 0, 0.1f);
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

		if (CanDamage)
		{
			FlameFadeInTimer += delta;
			FlameFadeInTimer = Math.Clamp(FlameFadeInTimer, 0, FLAME_FADE_IN_TIME);
		}

		Sprite.SpriteFrames.SetAnimationSpeed("default", Mathf.Lerp(SlowestFPS, FastestFPS, Mathf.Abs(NormalisedCharge)));

		//idk if i should be doing this every frame but whatever
		int frameCount = Sprite.SpriteFrames.GetFrameCount("default");
		float flameOpacity = CanDamage ? MathUtils.CubicEasing((float)(FlameFadeInTimer / FLAME_FADE_IN_TIME)) : 0;

		(Sprite.Material as ShaderMaterial).SetShaderParameter("FrameCount", frameCount);
		(Sprite.Material as ShaderMaterial).SetShaderParameter("FlameOpacity", flameOpacity);
		// GD.Print(MathUtils.VectorToAngle(LinearVelocity).ToString());

		WasOnFloorLastFrame = isOnFloor;
		InDamagingFlight = true;
	}

	public void Land()
	{
		NormalisedCharge = 0;
		FlameFadeInTimer = 0;
		InDamagingFlight = false;
	}

	public float HeightFromLaunch(double interpolant)
	{
		return -MathF.Abs((float)interpolant) * VERTICAL_BOOST_MULTIPLIER;
	}

	public override void _IntegrateForces(PhysicsDirectBodyState2D state)
	{

		int bossesHit = 0;
	
		for (int i = 0; i < state.GetContactCount(); i++)
		{
			GodotObject collider = state.GetContactColliderObject(i);
			if (collider is CharacterBody2D)
			{
				bossesHit++;
				if (justBounced) continue;

				Vector2 normal = state.GetContactLocalNormal(i);
				state.LinearVelocity = state.LinearVelocity.Bounce(normal);
				justBounced = true;
			}
		}

		if (bossesHit == 0) {
			justBounced = false;
		}
	}

	public void HitEnemy(Node2D enemy)
	{
		GD.Print(InDamagingFlight.ToString() + " final");

		if (!InDamagingFlight)
		{
			GD.Print("can't damage");
			HealthComp hp = GetNode<HealthComp>("HealthComp");
			hp.Damage(1);
			return;
		}

		HealthComp health = enemy.GetNodeOrNull<HealthComp>("HealthComp");

		if (health != null)
		{
			GD.Print("can damage");
			bool lethal = health.Damage(1);
			GD.Print("damage dealt, hp is " + health.GetHp().ToString() + "/" + health.GetMaxHp().ToString());			
		}
	}
}

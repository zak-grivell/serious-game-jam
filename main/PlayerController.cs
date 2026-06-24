using Godot;
using NewGameProject;
using System;

public partial class PlayerController : RigidBody2D
{
	[Export]
	private float LAUNCH_MAX_SPEED = 1000.0f;
	[Export]
	private float VERTICAL_BOOST_MULTIPLIER = 500f;
	[Export]
	private float CHARGE_RATE = 20.0f;
	[Export]
	private int SlowestFPS = 6;
	[Export]
	private int FastestFPS = 24;
	[Export]
	private float CameraChargingZoom = 1.2f;
	
	[Export] private CameraMovement cameraMovement;
	[Export] private double cameraPeakSpeed;
	[Export] private float cameraPeakCoef; // The scaling coefficient for the camera peaking

	//private Sprite2D Sprite;
	private AnimatedSprite2D Sprite;
	private AnimationPlayer an;

	private RayCast2D floorRaycast;
	private const float CHARGE_RATE = 5.0f;
	private const float DECHARGE_RATE = 200.0f;
	private double NormalisedCharge = 0;
	private ProgressBar LaunchBar;
	private Camera2D Camera;
	private bool CanDamage => Mathf.Abs((float)NormalisedCharge) > 0.99;
	private const double FLAME_FADE_IN_TIME = 0.8;
	private double FlameFadeInTimer = 0;
	private bool WasOnFloorLastFrame;
	private int FlightDirection;
	private CameraMovement camera;
	private HealthComp health;
	private int lastHealth;
	private CpuParticles2D particles;
	private Vector2 particleBaseOffset = new Vector2(-9, 15);

	public override void _Ready()
	{
		floorRaycast = GetNode<RayCast2D>("OnFloor");
		Sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		Camera = GetNode<Camera2D>("Camera2D");
		Sprite.SpriteFrames.SetAnimationSpeed("default", SlowestFPS);

		LaunchBar = GetNode<ProgressBar>("LaunchBar");
		LaunchBar.MaxValue = 1.0;
		LaunchBar.Value = 0.0;
		LaunchBar.Visible = false;
		FlameFadeInTimer = 0;
		GD.Print("HI");
		camera = GetNode<CameraMovement>("Camera2D");
		GD.Print("Camera found: " + camera);
		health = GetNode<HealthComp>("HealthComp");
		lastHealth = health.GetHp();
		health.HealthChanged += OnHealthChanged;
		particles = GetNode<CpuParticles2D>("ChargeParticles");
		particles.Emitting = false;
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
			if (direction == 1) {
				GD.Print(direction, "direction value");
				particles.Rotation = 116;
				particles.Scale = new Vector2(1, 1);
				particles.Position = new Vector2(-9, 16);
			}
			else if (direction == -1) {
				GD.Print(direction, "direction value");
				particles.Rotation = 129;
				particles.Scale = new Vector2(-1, -1);
				particles.Position = new Vector2(14, 18);
			}
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
				X = LinearVelocity.X + MathUtils.CubicEasing((float)NormalisedCharge) * LAUNCH_MAX_SPEED,
				Y = HeightFromLaunch(NormalisedCharge)
			};

			// AngularVelocity += (float)NormalisedCharge * 5; the spinnies

			Rotation = MathUtils.VectorToAngle(LinearVelocity) - FlightDirection * MathF.PI / 2;
			//NormalisedCharge = 0;
			LaunchBar.Value = 0;
			LaunchBar.Visible = false;
			particles.Emitting = false;
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
			particles.Emitting = true;
		}

		if (isCharging && Math.Abs(NormalisedCharge) > 0.7)
		{
			float zoom = (float)Mathf.Lerp(1.0, CameraChargingZoom, MathUtils.CubicEasing((float)Math.Abs(NormalisedCharge)));
			Camera.Zoom = new Vector2(zoom, zoom);
			
			cameraMovement.PeakCameraTowards(
				new Vector2((float) NormalisedCharge * cameraPeakCoef, 0),
				cameraPeakSpeed
			);
		}
		else
		{
			float zoom = (float)Mathf.Lerp(Camera.Zoom.X, 1f, 0.1f);
			Camera.Zoom = new Vector2(zoom, zoom);
			
			cameraMovement.PeakCameraTowards(
				Vector2.Zero,
				cameraPeakSpeed
			);
		}
		//if (Input.IsActionJustPressed("ui_accept"))
		//{
		//	LinearVelocity = LinearVelocity with
		//	{
		//		X = LinearVelocity.X,
		//		Y = JUMP_FORCE
		//	};
		//}

		if (CanDamage)
		{
			FlameFadeInTimer += delta;
			FlameFadeInTimer = Math.Clamp(FlameFadeInTimer, 0, FLAME_FADE_IN_TIME);
		}

		Sprite.SpriteFrames.SetAnimationSpeed("default", Mathf.Lerp(SlowestFPS, FastestFPS, Mathf.Abs(NormalisedCharge)));
		
		//idk if i should be doing this every frame but whatever
		int frameCount = Sprite.SpriteFrames.GetFrameCount("default");
		float flameOpacity = CanDamage ? MathUtils.CubicEasing((float)FlameFadeInterpolant()) : 0;

		(Sprite.Material as ShaderMaterial).SetShaderParameter("FrameCount", frameCount);
		(Sprite.Material as ShaderMaterial).SetShaderParameter("FlameOpacity", flameOpacity);

		// GD.Print(MathUtils.VectorToAngle(LinearVelocity).ToString());

		WasOnFloorLastFrame = isOnFloor;

		GD.Print(isOnFloor);
	}

	public void Land()
	{
		NormalisedCharge = 0;
		FlameFadeInTimer = 0;
	}

	public float HeightFromLaunch(double interpolant)
	{
		return -MathF.Abs((float)interpolant) * VERTICAL_BOOST_MULTIPLIER;
	}

	public double FlameFadeInterpolant()
	{
		return FlameFadeInTimer / FLAME_FADE_IN_TIME;

	}
	
	private void OnHealthChanged(int hp) {
		if (hp < lastHealth) {
			GD.Print("SHAKING BITCH");
			camera?.Shake(25f, 0.5f);
		}
		lastHealth = hp;
	}
	
}

using Godot;

public partial class PlayerController : RigidBody2D
{
	private const float SPIN_SPEED = 10.0f;
	private const float SPIN_FORCE = 10.0f;
	private const float JUMP_FORCE = -600.0f;

	private RayCast2D OnFloor;

	public override void _Ready() {
		OnFloor = GetNode<RayCast2D>("OnFloor");
	}

	public override void _PhysicsProcess(double delta)
	{
		float direction = Input.GetAxis("ui_left", "ui_right");

		OnFloor.Rotation = -Rotation;

		AngularVelocity = direction * SPIN_SPEED;

		LinearVelocity = LinearVelocity with
		{
			X = LinearVelocity.X + direction * SPIN_FORCE
		};

		if (Input.IsActionJustPressed("ui_accept") && OnFloor.IsColliding())
		{
			LinearVelocity = LinearVelocity with
			{
				Y = JUMP_FORCE
			};
		}
	}
}

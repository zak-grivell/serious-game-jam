using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NewGameProject.bosses.wheel_turner.attacks.test_attack
{    
	public partial class JumpAttack : RandomAttackingState
	{
		[Export] private CharacterBody2D AttackOwner;
		[Export] private RayCast2D floorRaycast;
		[Export] private float jumpStrength;
		private Vector2 JumpDestination;
		private float heightAbovePlayer = 300f;
		private GravityComp GravityNode;
		private RigidBody2D Player;
		private double TargetTime = 1.4;
		private double ElapsedTime = 0;
		public override void _Ready() {
			base._Ready();
			attack = new JumpAttackAttack(AttackOwner, jumpStrength);
		}
		
		private bool firstTime = true;
		private bool OnFloorLastFrame = false;
		
		public override void OnEnter() {
			firstTime = true;
			OnFloorLastFrame = false;
			GravityNode = GetNode<GravityComp>("../../GravityComp");
			Player = GetNode<RigidBody2D>("/root/Main/Scene/CharacterBody2D");
			GD.Print(GravityNode);
			GD.Print(Player);
			JumpDestination = Player.Position + new Vector2(0f, -heightAbovePlayer);
		}
		
		public override IState NextState(double delta)
		{
			Boolean isOnFloor = floorRaycast.IsColliding();
			ElapsedTime += delta;

			if (ElapsedTime < TargetTime)
			{
				AttackOwner.Position = MathUtils.Lerp(AttackOwner.Position, JumpDestination, (float)(ElapsedTime / TargetTime));
				// GD.Print("trying");
			}

			if(firstTime) GetAttack().Attack(delta);
			firstTime = false;
			AttackOwner.MoveAndSlide();
			if (isOnFloor && !OnFloorLastFrame)
			{
				return idleState;
			}

			OnFloorLastFrame = isOnFloor;

			return this;
		}
	}
}

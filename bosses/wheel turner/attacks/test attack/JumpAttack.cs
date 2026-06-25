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
		
		public override void _Ready() {
			base._Ready();
			attack = new JumpAttackAttack(AttackOwner);
		}
		
		private bool firstTime = true;
		private bool OnFloorLastFrame = false;
		
		public override void OnEnter() {
			firstTime = true;
			OnFloorLastFrame = false;
		}
		
		public override IState NextState(double delta)
		{
			Boolean isOnFloor = floorRaycast.IsColliding();
			
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

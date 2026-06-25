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
		[Export]
		private CharacterBody2D AttackOwner;
		private RayCast2D floorRaycast;
		private bool OnFloorLastFrame;
		public override void OnEnter()
		{
			floorRaycast = GetNode<RayCast2D>("BossOnFloor");
			base.OnEnter();
		}
		public override IState NextState(double delta)
		{
			Boolean isOnFloor = floorRaycast.IsColliding();

			if (timer == 0)
			{
				AttackOwner.Velocity = new Vector2(0f, 10f);
			}

			if (isOnFloor && !OnFloorLastFrame)
			{
				return idleState;
			}

			OnFloorLastFrame = isOnFloor;

			return this;
		}
	}
}

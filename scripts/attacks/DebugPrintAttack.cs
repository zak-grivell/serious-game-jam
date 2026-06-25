using Godot;
using System;

public partial class DebugPrintAttack : IAttack
{
	[Export] private string msg;
	
	public override void Attack() {
		GD.Print(msg);
		base.Attack();
	}
	
	public override bool CanAttack() => true;
}

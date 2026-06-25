using Godot;
using System;

public partial class DebugPrintAttack : IAttack
{
	[Export] private string msg;
	
	public DebugPrintAttack(string msg) {
		this.msg = msg;
	}
	
	public void Attack(double delta) {
		GD.Print(msg);
	}
	
	public bool CanAttack() => true;
}

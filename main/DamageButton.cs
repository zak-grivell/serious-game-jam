using Godot;
using System;

public partial class DamageButton : Button
{
	public override void _Ready()
	{
		Pressed += _on_pressed;
	}


	private void _on_pressed()
	{
		var player = GetTree().GetFirstNodeInGroup("player");
		var health = player.GetNode<HealthComp>("HealthComp");
		health.Damage(1);
	}
}

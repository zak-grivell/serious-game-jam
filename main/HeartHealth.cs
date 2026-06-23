// incomplete script!! will finish when damage/health is finsihed 
// as dont completely understand it and no way to test rn anyways

using Godot;
using System;

public partial class HeartHealth : CanvasLayer
{
	private TextureRect heart1;
	private TextureRect heart2;
	private TextureRect heart3;
	private HealthComp health;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		heart1 = GetNode<TextureRect>("HBoxContainer/heart1");
		heart2 = GetNode<TextureRect>("HBoxContainer/heart2");
		heart3 = GetNode<TextureRect>("HBoxContainer/heart3");
		var player = GetTree().GetFirstNodeInGroup("player");
		health = player.GetNode<HealthComp>("HealthComp");
		health.HealthChanged += UpdateHearts;
		UpdateHearts(health.GetHp());
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void UpdateHearts(int hp)
	{
		heart1.Visible = hp >= 1;
		heart2.Visible = hp >= 2;
		heart3.Visible = hp >= 3;
	}
}

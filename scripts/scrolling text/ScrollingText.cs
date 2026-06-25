using Godot;
using System;

public partial class ScrollingText : Node
{
	[Export] private Node2D posNode;
	[Export] private double scrollTime;
	[Export] private double scrollSpeed;
	private double currTime = 0;
	
	public override void _Process(double delta) {
		if(currTime >= scrollTime) return;
		Vector2 screenSize = DisplayServer.ScreenGetSize();
		posNode.Position = new Vector2(
			screenSize.X / 8,
			screenSize.Y - (float) (scrollSpeed * currTime)
		);
		currTime += delta;
	}
}

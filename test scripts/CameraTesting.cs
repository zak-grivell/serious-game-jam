using Godot;
using System;

public partial class CameraTesting : Node
{
	[Export] private CameraMovement cameraMovement;
	[Export] private double speed;

	public override void _Input(InputEvent @event)
	{
		if(@event is InputEventMouseButton ev) {
			Vector2 peakPos = ev.Position;
			Vector2 resSize = GetViewport().GetVisibleRect().Size;
			resSize *= new Vector2(0.5f, 0.5f);
			peakPos -= resSize;
			GD.Print("Peaking towards: " + peakPos);
			cameraMovement.PeakCameraTowards(peakPos, speed);
		}
	}
}

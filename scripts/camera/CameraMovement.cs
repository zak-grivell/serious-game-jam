using Godot;
using System;

public partial class CameraMovement : Camera2D
{
	[Export] private Vector2 maxPeak;
	private Vector2 peakOrigin = Vector2.Zero,
		peakTarget = Vector2.Zero;
	private double peakTime = 1, currPeakTime = 0;

	public override void _Process(double delta)
	{
		this.currPeakTime += delta;
		this.Position = peakOrigin.Lerp(
			peakTarget, 
			(float) Math.Min(currPeakTime/peakTime, 1));
	}
	
	public void PeakCameraTowards(Vector2 peakTarget, double speed) {
		this.peakOrigin = this.Position;
		this.peakTarget = peakTarget;
		this.currPeakTime = 0;
		this.peakTime = (peakTarget - peakOrigin).Length() / speed;
	}
}

using Godot;
using System;

public partial class CameraMovement : Camera2D
{
	[Export] private Vector2 maxPeak;
	private Vector2 peakOrigin = Vector2.Zero,
		peakTarget = Vector2.Zero;
	private double peakTime = 1, currPeakTime = 0;

	// shake variables
	private float shakeAmount = 0;
	private float shakeTimer = 0;
	private Vector2 basePosition;
	
	public override void _Ready()
	{
		basePosition = Position;
	}

	public override void _Process(double delta)
	{
		this.currPeakTime += delta;
		basePosition = peakOrigin.Lerp(
			peakTarget, 
			(float) Math.Min(currPeakTime/peakTime, 1));
			
		// shake logic (only if player takes damage)
		Vector2 shakeOffset = Vector2.Zero;
		if (shakeTimer > 0) {
			shakeTimer -= (float)delta;
			shakeOffset = new Vector2((float)GD.RandRange(-shakeAmount, shakeAmount), (float)GD.RandRange(-shakeAmount, shakeAmount));
			shakeAmount = Mathf.Lerp(shakeAmount, 0, (float)delta * 10);
		}
		Position = basePosition + shakeOffset;
	}
	
	
	public void PeakCameraTowards(Vector2 peakTarget, double speed) {
		this.peakOrigin = this.Position;
		this.peakTarget = peakTarget;
		this.currPeakTime = 10;
		this.peakTime = (peakTarget - peakOrigin).Length() / speed;
	}
	
	public void Shake(float amount, float duration) {
		shakeAmount = amount;
		shakeTimer = duration;
	}
}

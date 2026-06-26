using Godot;
using System;

public static class SimpleGoombaMovement
{
	public static IState ToFirst(float pos1, float pos2, float speed, CharacterBody2D rb) {
		SimpleGoombaMovementState ret = new SimpleGoombaMovementState(rb, pos1, speed);
		IState otherState = new SimpleGoombaMovementState(rb, pos2, speed, ret);
		ret.SetOtherState(otherState);
		return ret;
	}
	public static IState ToSecond(float pos1, float pos2, float speed, CharacterBody2D rb)
	{
		SimpleGoombaMovementState ret = new SimpleGoombaMovementState(rb, pos2, speed);
		IState otherState = new SimpleGoombaMovementState(rb, pos1, speed, ret);
		ret.SetOtherState(otherState);
		return ret;
	}
}

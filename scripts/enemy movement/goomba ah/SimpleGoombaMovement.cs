using Godot;
using System;

public static class SimpleGoombaMovement
{
    public static IState ToFirst(float pos1, float pos2, float precision, float speed, RigidBody2D rb) {
        SimpleGoombaMovementState ret = new SimpleGoombaMovementState(rb, pos1, precision, speed);
        IState otherState = new SimpleGoombaMovementState(rb, pos2, precision, speed, ret);
        ret.SetOtherState(otherState);
        return ret;
    }
    public static IState ToSecond(float pos1, float pos2, float precision, float speed, RigidBody2D rb)
    {
        SimpleGoombaMovementState ret = new SimpleGoombaMovementState(rb, pos2, precision, speed);
        IState otherState = new SimpleGoombaMovementState(rb, pos1, precision, speed, ret);
        ret.SetOtherState(otherState);
        return ret;
    }
}

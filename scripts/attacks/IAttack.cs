using Godot;
using System;

public interface IAttack
{
	public void Attack(double delta);
	public bool CanAttack();
}

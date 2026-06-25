using Godot;
using System;

public partial class HealthComp : Node
{
	// signals when health is changed for hearts
	public event Action<int> HealthChanged;
	
	[Export] private int maxHp;
	private int hp;

	// The amount of time the player spends being invulnerable after taking damage
	[Export] private double dmgITime;
	private double currDmgITime;

	[Export] private DeathComp deathComp;

	public override void _Ready()
	{
		hp = maxHp;
		currDmgITime = 0.0f;
		HealthChanged?.Invoke(hp);
	}

	public override void _Process(double delta) {
		if(this.currDmgITime > 0) this.currDmgITime -= delta;
	}

	public bool Damage(int dmg) {
		if (currDmgITime <= 0)
		{
			this.hp -= dmg;
			this.currDmgITime = dmgITime;
			HealthChanged?.Invoke(hp);
			if (this.hp <= 0) {
				deathComp.Die();
				return true;
					}
		}

		return false;
	}
	public int GetHp() => hp;
	public int GetMaxHp() => maxHp;
}

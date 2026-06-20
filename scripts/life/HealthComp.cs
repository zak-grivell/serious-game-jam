using Godot;
using System;

public partial class PlayerAttributes : Node
{
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
    }

	public override void _Process(double delta) {
		if(this.currDmgITime > 0) this.currDmgITime -= delta;
	}

	public void Damage(int dmg) {
		if (currDmgITime <= 0)
		{
			this.hp -= dmg;
			this.currDmgITime = dmgITime;
			if (this.hp <= 0)
			{
				deathComp.Die();
                return;
			}
		}
	}
	public int GetHp() => hp;
}

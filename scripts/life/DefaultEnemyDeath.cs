using Godot;
using System;

public partial class DefaultEnemyDeath : DeathComp
{
    [Export] private Node enemyNode;

    public override void Die() {
        base.Die(); // Does nothing but it makes me feel OOP safe ^v^
        enemyNode.QueueFree();
    }
}

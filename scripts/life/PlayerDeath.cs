using Godot;

public partial class PlayerDeath : DeathComp {
	
	public override void Die() {
		GetTree().ChangeSceneToFile("res://menus/death/death_scene.tscn");
	}
}

extends Control


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	var music = load("res://audio/menu demo.mp3")
	MusicManager.Play(music)


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass
	
func _on_back_pressed() -> void:
	get_tree().change_scene_to_file("res://main-menu/main-menu.tscn")

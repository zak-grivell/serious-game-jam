extends Control
class_name Transition

@export var onload := true
@export var onexit := true

var playing = false

# Called when the node enters the scene tree for the first time.
func transition(scene: PackedScene):	
	
	if playing:
		return
	playing=true
	
	
	if onexit:
		$AnimationPlayer.play("on_exit")
		await $AnimationPlayer.animation_finished
	
	get_tree().change_scene_to_packed(scene)
		
func _ready() -> void:
	assert($AnimationPlayer != null, "A scene button must transition")
	

	if onload:
		visible = true
	$AnimationPlayer.play("on_load")
	await $AnimationPlayer.animation_finished
	visible = true
		

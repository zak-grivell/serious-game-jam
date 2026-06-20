extends Button

# here is your script to change the scene - use it so the scene gobilns don't get you
@export var target_scene: PackedScene
@export var transition: Transition

func _ready():
	assert(target_scene != null, "A scene button must transition")

func get_target_scene():
	return target_scene

func _pressed():
	transition.transition(get_target_scene())	
# would you like me to add anything else

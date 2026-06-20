extends RigidBody2D

const SPIN_SPEED = 10.0
const JUMP_FORCE = -300.0

func _physics_process(delta):
	var direction = Input.get_axis("ui_left", "ui_right")

	# Spin left/right
	angular_velocity = direction * SPIN_SPEED

	# Jump
	if Input.is_action_just_pressed("ui_accept"):
		linear_velocity.y = JUMP_FORCE

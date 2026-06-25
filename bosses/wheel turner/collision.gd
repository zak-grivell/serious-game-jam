extends Area2D

var MyCSharpScript = load("res://main/PlayerController.cs")
var my_csharp_node = MyCSharpScript.new()

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass

func _on_body_entered(body: Node2D) -> void:
	print(body.name)
	if (get_parent().has_method("HitEnemy")):
		get_parent().HitEnemy(body)
		print("found damage method")
	else:
		print("no damage method found")
	
	

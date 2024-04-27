extends Area2D

signal hit
@export var speed = 400
var screen_size
var condicion = true

func _ready():
	screen_size = get_viewport_rect().size


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	var velocity = Vector2.ZERO
	
	if Input.is_action_pressed("Derecha"):
		velocity.x += 200
	if Input.is_action_pressed("Izquierda"):
		velocity.x -= 200
	if Input.is_action_pressed("Arriba"):
		velocity.y -= 200
	if Input.is_action_pressed("Abajo"):
		velocity.y += 200
		
	"""	
	if Input.is_action_pressed("Saltar"):
		
		$AnimatedSprite2D.animation = "Moviendose"
		condicion = true
		print("moviendose")
	if Input.is_action_pressed("Arriba"):
		$AnimatedSprite2D.animation = "Quieto"
		condicion = false
		print("quieto")
	"""
	
	
	position += velocity * delta
	position = position.clamp(Vector2.ZERO, screen_size)


func _on_body_entered(body):
	hit.emit()
	print("Colisiono")

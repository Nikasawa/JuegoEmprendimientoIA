extends RigidBody2D
var velocity = Vector2(250, 0.0)

# Called when the node enters the scene tree for the first time.
func _ready():
	var mob_types = $AnimatedSprite2D.sprite_frames.get_animation_names()
	$AnimatedSprite2D.play(mob_types[randi() % mob_types.size()])

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	var move = Vector2(250, 0).rotated(rotation)
	position += move * delta
	
func _on_visible_on_screen_notifier_2d_screen_exited():
	queue_free()
	
func orbitar(ejeX, ejeY):
	
	rotation = 0
	
	var diferenciaX = ejeX - position[0]
	var diferenciaY = ejeY - position[1]
	
	var hipotenusa = sqrt(diferenciaX ** 2 + diferenciaY ** 2)
	
	var angulo = asin(diferenciaX / hipotenusa) 
	
	const VUELTA_COMPLETA = 180 * (PI / 180)
	
	if diferenciaY >= 0:
		rotation += VUELTA_COMPLETA
		angulo = asin(diferenciaX / hipotenusa) * -1
		
	rotation += angulo

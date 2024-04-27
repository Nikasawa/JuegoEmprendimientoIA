extends Node

@export var mob_scene: PackedScene
var score

# Called when the node enters the scene tree for the first time.
func _ready():
	new_game()
	


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass
	
func new_game():
	score = 0
	$StartTimer.start()
	print("inicio")
	
func _on_jugador_hit():
	pass # Replace with function body.

func _on_mob_timer_timeout():
	
	var jugadorPosX = $Jugador.position[0]
	var jugadorPosY = $Jugador.position[1]
	
	var mob = mob_scene.instantiate()
	
	# Choose a random location on Path2D.
	var mob_spawn_location = $MobPath/MobSpawnLocation # Toma el path creado en la escena Main como posibles posiciones para spawnear
	mob_spawn_location.progress_ratio = randf() # Ubicacion en la que aparece

	# Set the mob's direction perpendicular to the path direction.
	var direction = 0 #mob_spawn_location.rotation + PI / 2

	# Set the mob's position to a random location.
	mob.position = mob_spawn_location.position
	
	# Add some randomness to the direction.
	direction += 0 # randf_range(-PI / 4, PI / 4)
	mob.rotation = direction

	# Choose the velocity for the mob.
	#var velocity = Vector2(randf_range(150.0, 250.0), 0.0)
	#mob.linear_velocity = velocity.rotated(direction)

	# Spawn the mob by adding it to the Main scene.
	add_child(mob)
	
	# Sacar las medidas de un angulo recto entre el jugador y el mob para que el mob
	# cambie su movimiento a la direccion del jugador
	
	# Diferencias entre las pasiciones en X y en Y
	var diferenciaX = 0
	var diferenciaY = 0
	
	# Sacar la diferencia en X
	if jugadorPosX > mob.position[0]:
		diferenciaX = jugadorPosX - mob.position[0]
	else:
		diferenciaX = mob.position[0] - jugadorPosX
		
	# Sacar la diferencia en Y
	if jugadorPosY > mob.position[1]:
		diferenciaY = jugadorPosY - mob.position[1]
	else:
		diferenciaY = mob.position[1] - jugadorPosY

	# Inicio calculando el triangulo rectangulo
	var catetoA = diferenciaX
	var catetoB = diferenciaY
	var hipotenusa = diferenciaX + diferenciaY
	
	$Line2D.position[0] = jugadorPosX
	$Line2D.position[1] = mob.position[0]
	$Line2D.scale[0] = diferenciaX 
	

func _on_start_timer_timeout():
	$MobTimer.start()
	print("start")
	$ScoreTimer.start()


func _on_score_timer_timeout():
	pass # Replace with function body.

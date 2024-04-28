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
	
func _on_jugador_hit():
	pass # Replace with function body.

func _on_mob_timer_timeout():
	
	var mob = mob_scene.instantiate()
	
	# Choose a random location on Path2D.
	var mob_spawn_location = $MobPath/MobSpawnLocation # Toma el path creado en la escena Main como posibles posiciones para spawnear
	mob_spawn_location.progress_ratio = randf() # Ubicacion en la que aparece
	
	mob.position[0] = 450
	mob.position[1] = 250

	# Guardar la posicion en X y en Y del jugador
	var jugadorPosX = $Jugador.position[0]
	var jugadorPosY = $Jugador.position[1]
	
	# Conseguir la distancia a la que se encuentra el mob del jugador
	var catetoA = 0
	var catetoB = 0
	
	if jugadorPosX > mob.position[0]:
		catetoA = jugadorPosX - mob.position[0]
	else:
		catetoA = mob.position[0] - jugadorPosX
	
	if jugadorPosY > mob.position[1]:
		catetoB = jugadorPosY - mob.position[1]
	else: 
		catetoB = mob.position[1] - jugadorPosY
	
	# Sacar la hipotenusa
	var hipotenusa = sqrt(catetoA ** 2 + catetoB ** 2)

	# Conseguir gamma por si solo. 
	# Se multiplica por 180/pi porque el resultado que devuelve esta en radianes, y es necesario pasarlo a grados.
	var gamma = asin(catetoB / hipotenusa) * (180/3.14159265358979323846)

	# Set the mob's direction perpendicular to the path direction.
	var direction = gamma # mob_spawn_location.rotation + PI / 2

	# Set the mob's position to a random location.
	mob.position = mob_spawn_location.position
	
	mob.rotation = direction

	# Choose the velocity for the mob.
	var velocity = Vector2(randf_range(150.0, 250.0), 0.0)
	mob.linear_velocity = velocity.rotated(direction)

	# Spawn the mob by adding it to the Main scene.
	add_child(mob)
	

func _on_start_timer_timeout():
	$MobTimer.start()
	$ScoreTimer.start()


func _on_score_timer_timeout():
	pass # Replace with function body.

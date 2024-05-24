extends Node

# Tomar la escena del mob
@export var mob_scene: PackedScene
var score

# Array donde se guardan todos los enemigos
var grupo = []

# Called when the node enters the scene tree for the first time.
func _ready():
	new_game()
	

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):

	# Se almacenan todas las entidades (solo enemigos por ahora), en el que se revisa uno por uno ejecutando funciones
	# Espero que exista una forma mas optima de pasar por cada uno de los bichos si se supone que el juego esta pensado para tener un monton de entidades a la vez	
	for entidades in grupo:
		entidades.look_at($Jugador.position) 
	
func new_game():
	score = 0
	$StartTimer.start()

func _on_mob_timer_timeout():
	
	var mob = mob_scene.instantiate()
	
	# Choose a random location on Path2D.
	var mob_spawn_location = $MobPath/MobSpawnLocation # Toma el path creado en la escena Main como posibles posiciones para spawnear
	mob_spawn_location.progress_ratio = randf()
	
	# Set the mob's position to a random location.
	mob.position = mob_spawn_location.position

	# Spawn the mob by adding it to the Main scene.
	add_child(mob)
	
	grupo.append(mob)

func _on_start_timer_timeout():
	$MobTimer.start()

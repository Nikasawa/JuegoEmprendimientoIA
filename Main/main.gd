# GlobalSignal.gd
extends Node

@export var mob_scene: PackedScene
var score

var NewPoint1 = 0
var NewPoint2 = 0

var angulo = 0
var spawn = 1



signal value_changed(Nuevo_valor)



# Called when the node enters the scene tree for the first time.
func _ready():
	new_game()

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	
	GlobalSignal.emit_signal('Nuevo_valor', $Jugador.position)
	
	$Line2D.clear_points()
	$Line2D.add_point(Vector2(NewPoint1, NewPoint2))
	$Line2D.add_point(Vector2($Jugador.position[0], $Jugador.position[1])) 
	$Line2D.add_point(Vector2($Jugador.position[0], NewPoint2)) # Diferencia en Y de la posicion entre el jugador y el mob
	$Line2D.add_point(Vector2(NewPoint1, NewPoint2)) # Diferencia en X de la posicion entre el jugador y el mob
	
	if Input.is_action_pressed("Saltar"):
		for x in range($Line2D.get_point_count()):
			print("Punto " + str(x) + "º:" + str($Line2D.get_point_position(x)))
			
	if spawn == 1:
		mobSpawn(delta)
		spawn = 0 
	
func new_game():
	score = 0
	$StartTimer.start()
	
func _on_jugador_hit():
	pass

func _on_mob_timer_timeout():
	pass
	
func mobSpawn(delta):
	
	var mob = mob_scene.instantiate()
	
	# Choose a random location on Path2D.
	# var mob_spawn_location = $MobPath/MobSpawnLocation # Toma el path creado en la escena Main como posibles posiciones para spawnear
	
	# Set the mob's position to a random location.
	mob.position = Vector2(350, 550) # mob_spawn_location.position

	# Spawn the mob by adding it to the Main scene.
	add_child(mob)
	
	NewPoint1 = mob.position[0]
	NewPoint2 = mob.position[1]
	
	#print("angulo: " + str(mob.rotation))
	#print("gamma: " + str(gamma))
	#print("catetoA: " + str(catetoA))
	#print("catetoB: " + str(catetoB))
	#print("hipotenusa: " + str(hipotenusa))
	

func _on_start_timer_timeout():
	$MobTimer.start()
	$ScoreTimer.start()


func _on_score_timer_timeout():
	pass # Replace with function body.

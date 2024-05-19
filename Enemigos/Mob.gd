extends RigidBody2D


# Called when the node enters the scene tree for the first time.
func _ready():
	GlobalSignal.connect('value_changed', self, 'on_value_changed')

func on_value_changed(Nuevo_valor):
	# Conseguir la distancia a la que se encuentra el mob del jugador
	
	var catetoA = 0
	var catetoB = 0
	
	# Guardar la posicion en X y en Y del jugador
	var jugadorPosX = Nuevo_valor[0]
	var jugadorPosY = Nuevo_valor[1]
	var multiplo = 1
	var lado = 0
	
	if jugadorPosX > position[0]:
		catetoA = jugadorPosX - position[0]
	else:
		catetoA = position[0] - jugadorPosX
		multiplo = -1
	
	if jugadorPosY > position[1]:
		catetoB = jugadorPosY - position[1]
		#lado = +45
	else: 
		catetoB = position[1] - jugadorPosY
	
	# Sacar la hipotenusa
	var hipotenusa = sqrt(catetoA ** 2 + catetoB ** 2)

	# Conseguir gamma por si solo. 
	# Se multiplica por 180/pi porque el resultado que devuelve esta en radianes, y es necesario pasarlo a grados.
	var gamma = asin(catetoB / hipotenusa)# * (180/3.14159265358979323846)

	# Set the mob's direction perpendicular to the path direction.
	var direction = 0 # mob_spawn_location.rotation + PI / 2
	
	rotation = gamma
	
	if jugadorPosY == position[1]:
		rotation = sin(0)
		
	if jugadorPosX == position[0]:
		rotation = sin(0)
		
	# Sirve multiplicarlo por delta cuando se le aplica movimiento
	# (Para que se vea mas fluido)
	rotation = rotation 
	# Choose the velocity for the mob.
	# randf_range(150, 350)
	var velocity = Vector2(0, 0)
	# mob.linear_velocity = velocity.rotated(direction)

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	
	on_value_changed()

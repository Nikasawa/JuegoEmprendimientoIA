using Godot;
using System;

public partial class Mob : RigidBody2D
{
	// Stats que calculo que vamos a usar en el Mob
	float armadura;
	int vida;
	float velMovimiento;
	public float daño;
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		
		RandomNumberGenerator RNG = new RandomNumberGenerator();
		
		armadura = (float)5.0;
		vida = 100;
		velMovimiento = (float)250;
		daño = RNG.RandiRange(5, 20);
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
		
		// Crear un vector de movimiento y rotarlo
		var move = new Vector2(150, 0).Rotated(Rotation);
		
		// Actualizar la posición del RigidBody2D
		Position += move * (float)delta;
		
	}
}

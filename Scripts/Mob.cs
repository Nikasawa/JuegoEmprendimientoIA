using Godot;
using System;

public partial class Mob : RigidBody2D
{
	public float rotation;
	public Vector2 posicion;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
		
		// Crear un vector de movimiento y rotarlo
		var move = new Vector2(150, 0).Rotated(Rotation);
		
		// Actualizar la posición del RigidBody2D
		Position += move * (float)delta; 
		
	}
}

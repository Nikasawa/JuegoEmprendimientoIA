using Godot;
using System;

public partial class AtaqueBasicoPJ : RigidBody2D
{
	public int daño;
	public float velocidad;
	public Vector2 posicion;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		posicion = new Vector2(0, 0);
		daño = 10;
		velocidad = (float)100.0;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
		
		var move = new Vector2(150, 0).Rotated(Rotation);
		
		// Actualizar la posición del RigidBody2D
		Position += move * (float)delta;
	}
}

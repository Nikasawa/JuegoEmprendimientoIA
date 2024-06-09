using Godot;
using System;

public partial class ataque_basico_pj : Area2D
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
		posicion.ProgressRatio = velocidad * (float)delta;
	}
}

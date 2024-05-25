using Godot;
using System;
using System.Collections.Generic;

public partial class main : Node{
	
	[Export] 
	public PackedScene MobScene { get; set; }
	public object jugador;
	public List<RigidBody2D> arrayMobs = new List<RigidBody2D>();
	public int puntos;

	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		newGame();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
		
		Area2D jugador = GetNode<Area2D>("Jugador");
		
		foreach(Node2D entidad in arrayMobs){
			entidad.LookAt(jugador.Position);
		}
	}
	
	public void newGame(){
		puntos = 0;
		Timer StartTimer = GetNode<Timer>("StartTimer");
		StartTimer.Start();
	}
	
	public void _on_start_timer_timeout(){
		Timer MobTimer = GetNode<Timer>("MobTimer");
		MobTimer.Start();
	}

	public void _on_mob_timer_timeout(){
		
	// Obtener la instancia de la escena "MobScene"
		Node mobSceneInstance = MobScene.Instance();
		
		// Verificar si la instancia es válida
		if (mobSceneInstance != null)
		{
			// Intentar obtener una referencia al nodo "Mob" en la instancia de la escena
			Mob mobNode = mobSceneInstance.GetNode<Mob>("MobNodeName");
			
			// Verificar si se obtuvo la referencia correctamente
			if (mobNode != null)
			{
				// Ahora puedes trabajar con el nodo "Mob" de la escena
				// Por ejemplo, puedes agregarlo a la escena actual
				AddChild(mobNode);
			}
			else
			{
				GD.Print("No se pudo obtener la referencia al nodo 'Mob'");
			}
		}
		else
		{
			GD.Print("No se pudo instanciar la escena 'MobScene'");
		}
}
}

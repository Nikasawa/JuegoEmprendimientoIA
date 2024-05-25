using Godot;
using System;
using System.Collections.Generic;

public partial class main : Node{
	
	[Export]
	public PackedScene MobScene { get; set; }
	public object jugador;
	public List<Node> arrayMobs = new List<Node>();
	public int puntos;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		newGame();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
		
		Area2D jugador = GetNode<Area2D>("Jugador");
		
		foreach(RigidBody2D entidad in arrayMobs){
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
		
		Mob mob = MobScene.Instantiate<Mob>();
			
		var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
		mobSpawnLocation.ProgressRatio = GD.Randf();

		mob.Position = mobSpawnLocation.Position;
		
		AddChild(mob);
		arrayMobs.Add(mob);
	}
}

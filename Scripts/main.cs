using Godot;
using System;

// Esto es necesario para que los arrays de C# funcionen con los nodos de Godot
using System.Collections.Generic; 

public partial class main : Node{
	
	[Export] // Se exporta la escena del mob (MobScebe)
	public PackedScene MobScene { get; set; } // No es necesario exportar la del jugador porque ya viene inherente a la escena Main
	
	// Declaracion variables
	// En el lenguaje base hay que aclarar que tipo de variable es antes de usarla (Ej: int, char, string)
	// Aca es igual, solo que tambien es posible usar los nodos/herramientas de Godot como tipo de variable
	public Jugador jugador;
	public List<Node> arrayMobs = new List<Node>(); // Array donde se almacenan los mobs
	public int puntos;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		newGame();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
		
		// Por cada entidad del array: arrayMobs, se aplica la logica dentro del foreach
		// Aclaracion: Los mobs dentro del array son del nodo tipo: RigidBody2D, por eso se declara a la variable entidad del mismo tipo
		foreach(Mob entidad in arrayMobs){
			entidad.LookAt(jugador.Position);
		}
		
		if(jugador.vida <= 0){
			newGame();
			
			foreach(Mob entidad in arrayMobs){
				entidad.QueueFree();
			}
			
			arrayMobs.Clear();
		}
	}
	
	// Aca se declaran todos los valores bases que debe tener todo al iniciar el juego por primera vez
	public void newGame(){
		
		jugador = GetNode<Jugador>("Jugador");
		
		puntos = 0; // Puntaje (Esto estaba en el ejemplo de la documentacion de Godot, pero podriamos cambiarlo por XP o lo que gusten)
		
		Timer StartTimer = GetNode<Timer>("StartTimer");
		
		// Al iniciar el juego, el jugador toma la posicion que tiene el Marker2D llamado "StartPosition"
		jugador.Position = GetNode<Marker2D>("StartPosition").Position;
		
		// Inicio del timer/contador "StartTimer"
		StartTimer.Start();
	
	}
	
	
	
	// Cuando el contador StartTimer termina, se ejecuta lo que esta dentro del void
	public void _on_start_timer_timeout(){
		Timer MobTimer = GetNode<Timer>("MobTimer");
		MobTimer.Start();
	}
	
	// Cuando el contador MobTimer termina, se ejecuta lo que esta dentro del void
	public void _on_mob_timer_timeout(){
		
		// Se instancia el objeto Mob, de la escena MobScene
		Mob mob = MobScene.Instantiate<Mob>();
			
		
		var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation"); // Se llama al nodo PathFollow2D
		mobSpawnLocation.ProgressRatio = GD.Randf(); // Se elige una posicion aleatoria del PathFollow2D
		mob.Position = mobSpawnLocation.Position; // Se asigna la posicion devuelta a la posicion del mob
		
		AddChild(mob); // Se agrega el objeto mob a la escena
		arrayMobs.Add(mob); // Se agrega el objeto mob a la escena donde se guardan todos los mobs
	}

	public void _on_jugador_body_entered(Mob body){
		jugador.recibirDaño((int)body.daño);
	}
}

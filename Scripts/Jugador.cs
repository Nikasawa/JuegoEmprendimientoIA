using Godot;
using System;

public partial class Jugador : Area2D{
	
	public int speed { get;set; } = 400;
	public Vector2 ScreenSize;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		
		ScreenSize = GetViewportRect().Size;
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
	
		var velocity = Vector2.Zero; // The player's movement vector.

		if (Input.IsActionPressed("Derecha")){
			velocity.X += 400;
		}
		
		if (Input.IsActionPressed("Izquierda")){
			velocity.X -= 400;
		}

		if (Input.IsActionPressed("Abajo")){
			velocity.Y += 400;
		}

		if (Input.IsActionPressed("Arriba")){
			velocity.Y -= 400;
		}
		
		Position += velocity * (float)delta;
		
		Position = new Vector2(
			x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
			y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
		);
	
	}
}

using Godot;
using System;

public partial class Jugador : Area2D{
	
	[Signal]
	public delegate void HitEventHandler();
	
	// Stats que calculo que vamos a usar en el PJ
	float armadura; // Reduce daño recibido
	int vida; // Vida maxima
	float velMovimiento; // Velocidad a al que se mueve el PJ
	float velAtaque; // Frecuencia con la que ataca
	float rango; // Distancia maxima a la que puede atacar
	float daño;
	float experiencia; // Cantidad de experiencia acumulada (Cuando sube de nivel vuelve a 0)
	int nivel; // Nivel del PJ
	
	bool invulnerabilidad;
	
	// Nodos
	Area2D jugador;
	Timer InvulnerabilidadTimer;
	
	// Temitas de la pantalla 
	public Vector2 ScreenSize;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		
		// Declaracion de estadisticas bases
		armadura = (float)10.0;
		vida = 100;
		velMovimiento = (float)350.0;
		velAtaque = (float)100.0;
		rango = (float)0.0;
		daño = (float)10.0;
		experiencia = (float)0.0;
		nivel = 0;
		invulnerabilidad = false;
		
		// Nodos del Jugador
		//jugador = GetNode<Area2D>("jugador");
		//jugador.Connect("body_entered", this, nameof(OnBodyEntered));
		
		InvulnerabilidadTimer = GetNode<Timer>("Invulnerabilidad");
		//InvulnerabilidadTimer.Connect("timeout", this, nameof(OnInvulnerabilidadTimeout));
		
		ScreenSize = GetViewportRect().Size;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
	
		var velocity = Vector2.Zero; // = (0.0, 0.0)

		if (Input.IsActionPressed("Derecha")){
			velocity.X += 1;
		}
		
		if (Input.IsActionPressed("Izquierda")){
			velocity.X -= 1;
		}

		if (Input.IsActionPressed("Abajo")){
			velocity.Y += 1;
		}

		if (Input.IsActionPressed("Arriba")){
			velocity.Y -= 1;
		}
		
		// Poniendo un tipo de dato entre parentecis, y antes de una variable, hace que cambie el tipo de variable 
		// velocity es un Vector2 con float: (float, float), delta por defecto viene como double, por eso es necesario la conversion antes de multiplicar
		Position += velocity * velMovimiento * (float)delta;
		
		// Limita la posicion del jugador para que no se salga de la pantalla
		Position = new Vector2(
			x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
			y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
		);
	}
	
	// Cuando entra en contacto con un enemigo:
	// Recibe daño y activa un umbral de tiempo donde no puede recibir mas daño
	// (El umbral es para que no se lo pijeen de una)
	public void recibirDaño(int daño){
		
		if(invulnerabilidad == false){
			
			EmitSignal(SignalName.Hit);
			
			vida -= daño; // Despues el 10 se cambia por el daño que haga el enemigo
			GD.Print(vida);
			
			invulnerabilidad = true;
			InvulnerabilidadTimer.Start();
		}
		
	}
	
	// Cuando el Timer termina el umbral de invulnerabilidad desaparece
	// El tiempo del Timer es: 1s (Puede que termine cambiandolo un poquito)
	private void OnInvulnerabilidadTimeout(){
		invulnerabilidad = false;
	}
}

using Godot;
using System;

public partial class Jugador : Area2D{
	
	[Signal]
	public delegate void HitEventHandler();
	
	// Stats que calculo que vamos a usar en el PJ
	public float armadura; // Reduce daño recibido
	public int vida; // Vida maxima
	public float velMovimiento; // Velocidad a al que se mueve el PJ
	public float velAtaque; // Frecuencia con la que ataca
	public float rango; // Distancia maxima a la que puede atacar
	public float daño;
	public float experiencia; // Cantidad de experiencia acumulada (Cuando sube de nivel vuelve a 0)
	public int nivel; // Nivel del PJ
	public bool jugando;
	public bool clickCoolDown;
	
	bool invulnerabilidad;
	
	// Nodos
	Area2D jugador;
	Timer InvulnerabilidadTimer;
	Timer clickTimer;
	
	// Temitas de la pantalla 
	public Vector2 ScreenSize;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		nuevoJuego();
	}
	
	public void nuevoJuego(){	
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
		jugando = true;
		clickCoolDown = true;
		
		// Nodos del Jugador
		//jugador = GetNode<Area2D>("jugador");
		//jugador.Connect("body_entered", this, nameof(OnBodyEntered));
		
		InvulnerabilidadTimer = GetNode<Timer>("Invulnerabilidad");
		clickTimer = GetNode<Timer>("ClickTimer");
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
		
		if(Input.IsActionJustPressed("Click") && clickCoolDown == true){
			GD.Print(GetViewport().GetMousePosition());
			clickCoolDown = false;
			clickTimer.Start();
		}
		
		
		// Poniendo un tipo de dato entre parentecis, y antes de una variable, hace que cambie el tipo de variable 
		// velocity es un Vector2 con float: (float, float), delta por defecto viene como double, por eso es necesario la conversion antes de multiplicar
		Position += velocity * velMovimiento * (float)delta;
		
		// Limita la posicion del jugador para que no se salga de la pantalla
		Position = new Vector2(
			x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
			y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
		);
		
		if(vida <= 0){
			nuevoJuego();
		}
		
		
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


private void _on_click_cool_down_timeout(){
	clickCoolDown = true;
	}
}

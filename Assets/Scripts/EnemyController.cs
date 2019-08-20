using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* Clase que controla el enemigo
*/
public class EnemyController : MonoBehaviour{
	/**
	* Declarar variables de modificación del enemigo
	*/
	public float vida = 150f;
	private float maxVida = 150f;
	public GameObject laserEnemigo;
	public float velDisparo = 7f;
	public float disparoXSegundo = 0.8f;
	public float padding = 0;
	public float speed = 5f;
	public float direction = 1f;
	private float limMax;
	private float limMin;
	public float width;
	private Animator animator;
	
	void Start(){
		animator = GetComponent<Animator>();
		/**
		* configuración de la cámara para no salir de los bordes
		*/
		Camera camera = Camera.main;
		float distance = transform.position.z - camera.transform.position.z;
		limMin = camera.ViewportToWorldPoint(new Vector3(0,0,distance)).x;
		limMax = camera.ViewportToWorldPoint(new Vector3(1,1,distance)).x;
		transform.localRotation = Quaternion.Euler(0, 0, 90);
		transform.localScale = new Vector3(1,1,1);
		width = GameObject.Find("Spawn").GetComponent<EnemySpawner>().width;

	}

	/**
	* Al actualizar los frames, disparar segun la frecuencia configurada
	*/
	void Update(){

		float frecuencia = Time.deltaTime * disparoXSegundo;
		if(Random.value < frecuencia)
			Fire();

		/**
		* Hacer movimiento del spawner de derecha a izquierda
		* Evitar que el spawner pase de los límites
		*/
		float legionBorIzq = transform.position.x - (0.1f * width);
		float legionBorDer = transform.position.x + (0.1f * width);
		//Comprobar la direccion del enemigo y girarlo
		if(legionBorIzq < limMin){
			direction = 1f;
			transform.localRotation = Quaternion.Euler(0, 0, 90);
		}else if(legionBorDer > limMax){
			direction = -1f;
			transform.localRotation = Quaternion.Euler(0, 0, -90);
		}
		transform.position += new Vector3(direction * speed * Time.deltaTime, 0,0);
	}

	/**
	* Accion del disparo que se realiza
	*/
	void Fire(){
		Vector3 posicionDisparo = transform.position + new Vector3(0,-1,0);
		// Instanciar la bala controller
		GameObject balaEnemiga = Instantiate(laserEnemigo, posicionDisparo, Quaternion.identity) as GameObject;
		balaEnemiga.GetComponent<Rigidbody2D>().velocity = new Vector3(0,-velDisparo,0);
	}

	/**
	* Ejecutar la colision de la bala con el objeto
	* Calcular la vida y destruir el objeto si su vida es 0
	*/
	void OnTriggerEnter2D(Collider2D collider){
		BalaController bala = collider.gameObject.GetComponent<BalaController>();
		PlayerController Player = collider.gameObject.GetComponent<PlayerController>();
		if(bala){
			vida -= bala.recibeDanio();
			animator.SetFloat("vida", vida);
			bala.Golpe();
			GameObject.Find("Canvas").GetComponent<GameController>().Record(bala.recibeDanio());
		}else if(Player){
			vida -= 15;
			animator.SetFloat("vida", vida);
		}
		if(vida <= 0){
			Destroy(gameObject);
		}
	}

	void OnGUI(){
		Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);

		float vidaParcial = (vida/maxVida) * 100;
		int vidaFinal = (int)vidaParcial;
		GUI.Box(
			new Rect(
				pos.x - 20, //Posicion X de la barra
				Screen.height - pos.y - 40, //Posicion Y de la barra
				40, //Ancho de la barra
				24 //Alto de la barra
			), vidaFinal.ToString()); //Texto a mostrar
	}
}

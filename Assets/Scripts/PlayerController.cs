using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
* Clase que controla el personaje
*/
public class PlayerController : MonoBehaviour{
	/**
	* Declarar variables de modificación del personaje
	*/
	public GameObject disparoPrefab;
	public GameObject barraVida;
	public float velDisparo = 6f;
	public float speed = 2f;
	public float padding = 1;
	private float xmax=-5;
	private float xmin=5;
	private float ymax=-5;
	private float ymin=5;
	private float repDisparo = 3f;
	public float vida = 420f;
	private Animator animator;

	/**
	* configuración de la cámara para no salir de los bordes
	*/
	void Start(){
		animator = GetComponent<Animator>();
		Camera camera = Camera.main;
		float distance = transform.position.z - camera.transform.position.z;
		xmin=camera.ViewportToWorldPoint(new Vector3(0,0,distance)).x+padding;
		xmax=camera.ViewportToWorldPoint(new Vector3(1,1,distance)).x-padding;
		ymin=camera.ViewportToWorldPoint(new Vector3(0,0,distance)).y+padding;
		ymax=camera.ViewportToWorldPoint(new Vector3(1,1,distance)).y-padding;
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.Space)){
			gameObject.layer = LayerMask.NameToLayer("playerUnder");
			animator.SetBool("underSea", true);
		}
		if(Input.GetKeyUp(KeyCode.Space)){
			gameObject.layer = LayerMask.NameToLayer("Player");
			animator.SetBool("underSea", false);
		}
		// Ejecutar disparo mientras se presiona espacio
		if(Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space))
			InvokeRepeating("Disparo",0.0001f,repDisparo);
		// Cancelar disparo cuando se suelta espacio
		if(Input.GetKeyUp(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.Space))
			CancelInvoke("Disparo");
		
		//Movimiento horizontal
		float hInput = Input.GetAxis("Horizontal");
		transform.position += new Vector3(hInput * speed * Time.deltaTime,0,0);

		//Movimiento vertical
		float vInput = Input.GetAxis("Vertical");
		transform.position += new Vector3(0,vInput * speed * Time.deltaTime,0);
		
		//Rectificar clamping para que no se salga de los bordes
		float newX = Mathf.Clamp(transform.position.x,xmin,xmax);
		float newY = Mathf.Clamp(transform.position.y,ymin,ymax);
		transform.position = new Vector3(newX,newY,transform.position.z);

		//Metodo de girar nave con el mouse
		girarNave();
	}

	/**
	* Accion del disparo que se realiza
	*/
	void Disparo(){
		Vector3 posicionDisparo = new Vector3(0,0,0);
		// Instanciar la bala controller
		GameObject disparo = Instantiate(disparoPrefab, transform.position + posicionDisparo, Quaternion.identity) as GameObject;

		//Obtener la posicion del mouse
		Vector3 mousePosition = Input.mousePosition;
		//Posicionar el puntero dentro del área de la cámara
		mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
		//Orientar la dirección hacia el puntero
		Vector2 direction = new Vector2(
			mousePosition.x - transform.position.x * velDisparo,
			mousePosition.y - transform.position.y * velDisparo
		);
		disparo.GetComponent<Rigidbody2D>().velocity = direction;
	}

	/**
	* Ejecutar la colision de la bala con el objeto
	* Calcular la vida y destruir el objeto si su vida es 0
	*/
	void OnTriggerEnter2D(Collider2D collider){
		BalaController bala = collider.gameObject.GetComponent<BalaController>();
		EnemyController enemigo = collider.gameObject.GetComponent<EnemyController>();
		if(bala){
			vida -= bala.recibeDanio();
			animator.SetFloat("vida", vida);
			bala.Golpe();
			barraVida.GetComponent<barraVida>().tomarDaño(bala.recibeDanio());
		}else if(enemigo){
			vida -= 15;
			animator.SetFloat("vida", vida);
			barraVida.GetComponent<barraVida>().tomarDaño(15);
		}
		if(vida <= 0){
			SceneManager.LoadScene("Lose Screen");
		}
	}

	/**
	* Girar la nave con la posición del mouse
	*/
	void girarNave(){
		//Obtener la posicion del mouse
		Vector3 mousePosition = Input.mousePosition;
		//Posicionar el puntero dentro del área de la cámara
		mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
		//Orientar la dirección hacia el puntero
		Vector2 direction = new Vector2(
			mousePosition.x - transform.position.x,
			mousePosition.y - transform.position.y
		);
		transform.up = direction;
	}

}

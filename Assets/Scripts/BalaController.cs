using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* clase general que controla las balas
*/
public class BalaController : MonoBehaviour{
	/*
	* Metodos publicos de daño
	* En la clase que usa a la bala, se le aplicaran estos metodos
	*/
	// Se define un daño de la bala
	public float Danio = 80f;
	// Al momento de la colision se recibe un daño
	public float recibeDanio(){ return Danio; }
	// Cuando el daño supera la vida, se destruye el objeto
	public void Golpe(){ Destroy(gameObject); }
}

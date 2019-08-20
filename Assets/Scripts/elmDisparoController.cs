using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* clase general que elimina los disparos realizados
*/
public class elmDisparoController : MonoBehaviour{
	/*
	* Al momento de la colision
	*/
	void OnTriggerEnter2D(Collider2D col){
		// Destruir el objeto que lo toca
		Destroy(col.gameObject);
	}
}

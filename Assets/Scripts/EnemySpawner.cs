using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
* clase general del generador de enemigos
*/
public class EnemySpawner : MonoBehaviour{
	/**
	* Declarar variables de modificación del generador de enemigos
	*/
	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	public string nextLvl;


	bool AllMembersAreDead(){
		foreach(Transform position in transform){
			if(position.childCount > 0)
				return false;
		}
		return true;
	}

	void Update(){
		if(AllMembersAreDead())
			SceneManager.LoadScene(nextLvl);
			//print("todos los enemigos han sido acribillados joder");
	}

	void Start(){

		/**
		* Generar enemigos en los puntos del espacio
		*/
		foreach(Transform child in transform){
			GameObject Enemy = Instantiate(enemyPrefab, child.position, Quaternion.identity) as GameObject;
			Enemy.transform.parent = child;
		}
	}


	/**
	* Dibujar circulos que orienten al diseñador para poner a los enemigos
	*/
	void OnDrawGizmos(){
		float xmin = transform.position.x - (0.5f * width);
		float xmax = transform.position.x + (0.5f * width);
		float ymin = transform.position.y - (0.5f * height);
		float ymax = transform.position.y + (0.5f * height);

		Gizmos.DrawLine(new Vector3(xmin, ymin, 0), new Vector3(xmin, ymax, 0));
		Gizmos.DrawLine(new Vector3(xmin, ymax, 0), new Vector3(xmax, ymax, 0));
		Gizmos.DrawLine(new Vector3(xmax, ymax, 0), new Vector3(xmax, ymin, 0));
		Gizmos.DrawLine(new Vector3(xmax, ymin, 0), new Vector3(xmin, ymin, 0));
	}
}

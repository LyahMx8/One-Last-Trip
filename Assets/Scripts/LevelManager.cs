using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/**
* Clase que controla los niveles
*/
public class LevelManager : MonoBehaviour {

	public void LoadLevel(string name){
		//Debug.Log ("New Level load: " + name);
		SceneManager.LoadScene (name);
	}

	public void QuitRequest(){
		//Debug.Log ("Quit requested");
		Application.Quit ();
	}

}

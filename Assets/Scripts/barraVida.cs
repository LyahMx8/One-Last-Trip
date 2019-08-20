using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class barraVida : MonoBehaviour{

	public Image Vida;
	public float hp;
	public float hpMaximo = 420f;
	public GameObject txt;

	void Start(){
		hp = hpMaximo;
		txt.GetComponent<UnityEngine.UI.Text>().text = "100%";
	}

	public void tomarDaño( float amount ){
		hp = Mathf.Clamp(hp-amount, 0f, hpMaximo);
		Vida.transform.localScale = new Vector2(hp/hpMaximo,1);
		float vidaParcial = (hp/hpMaximo) * 100;
		int vidaFinal = (int)vidaParcial;
		txt.GetComponent<UnityEngine.UI.Text>().text = vidaFinal.ToString()+"%";
	}
}

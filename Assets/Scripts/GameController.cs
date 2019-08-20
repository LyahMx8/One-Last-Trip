using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour{
	[Range (0f, 0.20f)]
	public float parallaxSpeed = 0.02f;
	public RawImage background;
	public Text recordText;
	public int bckDirection = 0;
	private float record;

	void Start(){
	}

	// Update is called once per frame
	void Update(){
		float finalSpeed = parallaxSpeed * Time.deltaTime;
		if(bckDirection == 0){
			background.uvRect = new Rect(background.uvRect.x + finalSpeed, 0f, 1f, 1f);
		}else{
			background.uvRect = new Rect(0f, background.uvRect.y + finalSpeed, 1f, 1f);
		}
		//print(PlayerPrefs.GetFloat("Max Points",0));
		recordText.text = "Record: " + PlayerPrefs.GetFloat("Max Points",0).ToString();
	}

	public void Record(float sumaPuntos){
		record = PlayerPrefs.GetFloat("Max Points",0) + sumaPuntos;
		PlayerPrefs.SetFloat("Max Points",record);
	}

	/*public void IncreasePoints(){
		if(points >= GetMaxScore()){
			recordText.text = "Mejor puntaje: " + GetMaxScore().ToString();
			SaveScore(points);
		}
	}
	public int GetMaxScore(){
		return ;
	}

	public int SaveScore(int currentPoints){
		return 
	}*/
}

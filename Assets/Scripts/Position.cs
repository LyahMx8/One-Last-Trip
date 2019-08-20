using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour{
	// Start is called before the first frame update
	void OnDrawGizmos(){
		Gizmos.DrawWireSphere(transform.position, 1);
	}
}

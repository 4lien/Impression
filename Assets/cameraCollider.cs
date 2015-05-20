using UnityEngine;
using System.Collections;

public class cameraCollider : MonoBehaviour {
	CharacterMotor motor;
	void Start () {
		motor=transform.parent.GetComponent<CharacterMotor>();
	}
	
	// Update is called once per frame
	void OnTriggerStay(Collider p){
		Debug.Log (p);
	}
}

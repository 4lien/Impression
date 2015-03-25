using UnityEngine;
using System.Collections;

public class temp : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos=transform.position-Camera.main.transform.position;
		Debug.Log (pos);
	}
}

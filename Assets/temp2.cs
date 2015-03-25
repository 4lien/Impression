using UnityEngine;
using System.Collections;

public class temp2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 c=Camera.main.transform.position;
		Vector3 p = this.transform.localPosition;
		Debug.Log (p);

		//Camera.main.transform.localPosition = new Vector3 (p.x,p.y+0.6f,p.z+0.4f);

	}
}

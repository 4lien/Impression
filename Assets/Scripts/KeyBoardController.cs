using UnityEngine;
using System.Collections;

public class KeyBoardController : MonoBehaviour {
	private FirstPersonController con;
	void Start(){
		con = transform.parent.gameObject.transform.FindChild ("Player").gameObject.GetComponent<FirstPersonController>();

	}
	void Update(){
		
		con.left = Input.GetKey ("a");
		con.right = Input.GetKey ("d");
		con.backward = Input.GetKey ("s");
		con.forward = Input.GetKey ("w");
		con.shooting = Input.GetMouseButton (0);
		con.jumping = Input.GetKey ("space");
	}
}
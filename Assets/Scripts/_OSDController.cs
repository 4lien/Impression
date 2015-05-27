using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterMotor))]
public class _OSDController : MonoBehaviour {
	private FirstPersonController con;
	void Start(){
		con = transform.parent.gameObject.transform.FindChild ("Player").gameObject.GetComponent<FirstPersonController>();
	}
	public GameObject player;
	public void onForwardDown(){
		con.forward = true;
	}
	public void onForwardUP(){
		con.forward = false;
	}
	public void onBackDown(){
		con.backward = true;
	}
	public void onBackUP(){
		con.backward = false;
	}
	public void onLeftDown(){
		con.left = true;
	}
	public void onLeftUP(){
		con.left = false;
	}
	public void onRightDown(){
		con.right = true;
	}
	public void onRightUP(){
		con.right = false;
	}
	public void onFireDown(){
		con.shooting= true;
	}
	public void onFireUp(){
		con.shooting = false;
	}
	public void onJumpDown(){
		con.jumping = true;
	}
	public void onJumpUp(){
		con.jumping = false;
	}
}

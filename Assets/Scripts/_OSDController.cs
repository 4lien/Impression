using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterMotor))]
public class _OSDController : MonoBehaviour {
	private bool isForDown = false;
	private bool isBackDown = false;
	private bool isLeftDown = false;
	private bool isRightDown = false;
	private bool isFireDown=false;
	private bool isJumpDown=false;
	public GameObject player;
	CharacterMotor motor;
	void Start(){
		motor=player.GetComponent<CharacterMotor>();
	}
	public void onForwardDown(){
		isForDown = true;
	}
	public void onForwardUP(){
		isForDown = false;
	}
	public void onBackDown(){
		isBackDown = true;
	}
	public void onBackUP(){
		isBackDown = false;
	}
	public void onLeftDown(){
		isLeftDown = true;
	}
	public void onLeftUP(){
		isLeftDown = false;
	}
	public void onRightDown(){
		isRightDown = true;
	}
	public void onRightUP(){
		isRightDown = false;
	}
	public void onFireDown(){
		isFireDown = true;
	}
	public void onFireUp(){
		isFireDown = false;
	}
	public void onJumpDown(){
		isJumpDown = true;
	}
	public void onJumpUp(){
		isJumpDown = false;
	}
	void Update(){
		player.SendMessage ("MoveForward",isForDown);
		player.SendMessage ("MoveBack",isBackDown);
		player.SendMessage ("MoveLeft",isLeftDown);
		player.SendMessage ("MoveRight",isRightDown);
		player.SendMessage ("Fire", isFireDown);
		motor.inputJump = isJumpDown;
		
	}
}

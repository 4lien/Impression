using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterMotor))]
public class _BTController : MonoBehaviour {
	private bool isForDown = false;
	private bool isBackDown = false;
	private bool isLeftDown = false;
	private bool isRightDown = false;
	private bool isFireDown=false;
	private bool isJumpDown=false;
	CharacterMotor motor;
	GameObject player;
	void Start(){
		player=GameObject.Find("First Person Controller");
		motor=player.GetComponent<CharacterMotor>();
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

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
	public GameObject player;
	void Update(){
		player.SendMessage ("MoveForward",isForDown);
		player.SendMessage ("MoveBack",isBackDown);
		player.SendMessage ("MoveLeft",isLeftDown);
		player.SendMessage ("MoveRight",isRightDown);
		player.SendMessage ("Fire", isFireDown);
		player.SendMessage ("Jump",isJumpDown);
	}
}

using UnityEngine;
using System.Collections;

public class _BTController : MonoBehaviour {
	private bool isForDown = false;
	private bool isBackDown = false;
	private bool isLeftDown = false;
	private bool isRightDown = false;
	private bool isFireDown=false;
	GameObject player;
	void Start(){
		player=GameObject.Find("First Person Controller");
	}
	void Update(){




		player.SendMessage ("MoveForward",isForDown);
		player.SendMessage ("MoveBack",isBackDown);
		player.SendMessage ("MoveLeft",isLeftDown);
		player.SendMessage ("MoveRight",isRightDown);
		player.SendMessage ("Fire", isFireDown);
		
	}
}

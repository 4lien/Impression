using UnityEngine;
using System.Collections;

public class _BTController : MonoBehaviour {

	private bool isForDown = false; //u
	private bool isBackDown = false; //d
	private bool isLeftDown = false; //l
	private bool isRightDown = false; //r
	private bool isFireDown=false; //s
	private bool isJumpDown=false; //j
	private char[] buttonRead;
	public GameObject player;
	void Start(){
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	void Update(){
		
		
		
		BlutoothManager.getBTData ();
		buttonRead= ForBluetoothState.getData.ToCharArray();
		
		foreach (char button in buttonRead){
			
			switch(button){
			case 'q' :
				isLeftDown = false;

				break;
			case 'x' :
				isRightDown = false; 
				break;
			case 'k' :
				isBackDown = false;
				break;
			case 'r' :
				isForDown = false; 
				break;
			case 's' :
				isFireDown = false; 
				break;
			case 'j' :
				isJumpDown = false; 
				break;
			case 'Q' :
				isLeftDown = true; 
				break;
			case 'X' :
				isRightDown = true;
					
				break;
			case 'K' :
				isBackDown = true;
				break;
			case 'R' :
				isForDown = true; 
				break;
			case 'S' :
				isFireDown = true; 
				break;
			case 'J' :
				isJumpDown = true; 
				break;
			}
		}
		
		
		
		
		player.SendMessage ("MoveForward",isForDown);
		player.SendMessage ("MoveBack",isBackDown);
		player.SendMessage ("MoveLeft",isLeftDown);
		player.SendMessage ("MoveRight",isRightDown);
		player.SendMessage ("Fire", isFireDown);
		player.SendMessage ("Jump",isJumpDown);

		
	}
}
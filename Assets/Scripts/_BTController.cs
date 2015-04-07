using UnityEngine;
using System.Collections;

public class _BTController : MonoBehaviour {
	private bool isForDown = false; //u
	private bool isBackDown = false; //d
	private bool isLeftDown = false; //l
	private bool isRightDown = false; //r
	private bool isFireDown=false; //s
	private char[] buttonRead;
	GameObject player;
	void Start(){
		player=GameObject.Find("First Person Controller");
	}
	void Update(){
		
		
		
		BlutoothManager.getBTData ();
		buttonRead= ForBluetoothState.getData.ToCharArray();
		
		foreach (char button in buttonRead){
			
			switch(button){
			case 'u' :
				isForDown = false; 
				break;
			case 'd' :
				isBackDown = false; 
				break;
			case 'l' :
				isLeftDown = false; 
				break;
			case 'r' :
				isRightDown = false; 
				break;
			case 's' :
				isFireDown = false; 
				break;
			case 'U' :
				isForDown = true; 
				break;
			case 'D' :
				isBackDown = true; 
				break;
			case 'L' :
				isLeftDown = true; 
				break;
			case 'R' :
				isRightDown = true; 
				break;
			case 'S' :
				isFireDown = true; 
				break;
			}
		}
		
		
		
		
		player.SendMessage ("MoveForward",isForDown);
		player.SendMessage ("MoveBack",isBackDown);
		player.SendMessage ("MoveLeft",isLeftDown);
		player.SendMessage ("MoveRight",isRightDown);
		player.SendMessage ("Fire", isFireDown);
		
	}
}

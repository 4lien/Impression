using UnityEngine;
using System.Collections;

public class _BTController : MonoBehaviour {

	private char[] buttonRead;
	private FirstPersonController con;
	void Start(){
		con = GameObject.FindGameObjectWithTag ("Player").GetComponent<FirstPersonController> ();
	}
	void Update(){
		BlutoothManager.getBTData ();
		buttonRead= ForBluetoothState.getData.ToCharArray();
		foreach (char button in buttonRead){
			switch(button){
			case 'q' :
				con.left=false;
				break;
			case 'x' :
				con.right=false;
				break;
			case 'k' :
				con.backward=false;
				break;
			case 'r' :
				con.forward=false;
				break;
			case 's' :
				con.shooting=false;
				break;
			case 'j' :
				con.jumping=false;
				break;
			case 'Q' :
				con.left=true;
				break;
			case 'X' :
				con.right=true;
				break;
			case 'K' :
				con.backward=true;
				break;
			case 'R' :
				con.forward=true;
				break;
			case 'S' :
				con.shooting=true;
				break;
			case 'J' :
				con.jumping=true;
				break;
			}
		}
	}
}
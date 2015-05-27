using UnityEngine;
using System.Collections;

public class NetWorkManager : MonoBehaviour {}
/*	
	// Use this for initialization
	void Start () {
		connect ();
		
	}
	void connect(){
		//PhotonNetwork.offlineMode = true;
		PhotonNetwork.ConnectUsingSettings ("0.0.1");
		
	}
	
	void OnGUI(){
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ());
	}
	
	void OnJoinedLobby(){
		PhotonNetwork.JoinRandomRoom ();
	}
	
	void OnPhotonRandomJoinFailed(){
		PhotonNetwork.CreateRoom (null);
	}
	void OnJoinedRoom(){
		Debug.Log ("OnJoinedRoom");
		SpawnMyPlayer ();
	}
	
	void SpawnMyPlayer(){
		GameObject GameSet=(GameObject)PhotonNetwork.Instantiate ("GameSet",Vector3.zero,Quaternion.identity,0);
		GameObject OSD=GameSet.transform.FindChild("OSD").gameObject;
		GameObject _KeyController=GameSet.transform.FindChild("_KeyController").gameObject;
		GameObject Player=GameSet.transform.FindChild("Player").gameObject;
		GameObject camera=Player.transform.FindChild("Main Camera").gameObject;
		GameObject EventSystem=GameSet.transform.FindChild("EventSystem").gameObject;
		EventSystem.SetActive (true);
		_KeyController.SetActive (true);
		OSD.SetActive (true);
		camera.SetActive (true);
		((MonoBehaviour)Player.GetComponent<FirstPersonController> ()).enabled = true;
		((MonoBehaviour)Player.GetComponent<CharacterMotor> ()).enabled = true;
	}
}*/

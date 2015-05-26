using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Exit : MonoBehaviour {
	public GameObject player;
	public GameObject info;
	void OnTriggerStay(Collider p) {	//범위안에 플레이어 포착
		if(player.transform!=p.transform)	//플레이어가 아니거나 죽었으면 안함
			return;
		info.SetActive (true);
		StartCoroutine (destroy());
	}
	
	IEnumerator destroy(){
		yield return new WaitForSeconds(5f);
		Application.LoadLevel (Application.loadedLevelName);
	}
}

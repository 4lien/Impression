using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Exit : MonoBehaviour {
	public GameObject player;
	public GameObject info;
	private Text infoText;
	private FirstPersonController con;
	private bool hit=false;
	void Start(){
		con = player.GetComponent<FirstPersonController>();
		infoText = info.GetComponent<Text> ();
	}
	void OnTriggerStay(Collider p) {	//범위안에 플레이어 포착
		if(player.transform!=p.transform)	//플레이어가 아니거나 죽었으면 안함
			return;
		info.SetActive (true);
		if (!hit) {
			hit=true;
			StartCoroutine (gameClear ());
		}
	}
	
	IEnumerator gameClear(){
		infoText.text ="Game Clear";
		infoText.text += "\r\nScore:" + con.getScore ()+" ("+con.getKilled()+")";
		yield return new WaitForSeconds(5f);	//5초 기다린 후 
		infoText.text+="\r\n\r\nclick to reset";
		while (!con.shooting) {	//클릭이 될때까지
			yield return null;	//기다림
		}
		Application.LoadLevel (Application.loadedLevelName);	//클릭이 됐으면 리셋
	}
}

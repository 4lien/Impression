using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressBar : MonoBehaviour {
	public float percent=0f;
	RawImage gage;
	// Use this for initialization
	void Start () {
		gage = transform.FindChild ("gage").GetComponent<RawImage> ();
	} 
	
	// Update is called once per frame
	void Update () {
		if (percent >= 1f)
			percent = 1f;
		Vector3 t = gage.transform.localScale;
		t.x = percent;
		gage.transform.localScale = t;
	}
	public void on(){	//활성화
		this.enabled = true;
		transform.localScale = new Vector3 (1f, 1f, 1f);
	}
	public void off(){ //비활성화
		this.enabled = false;
		transform.localScale = new Vector3 (0f, 0f, 0f);
	}

}

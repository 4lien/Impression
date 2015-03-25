using UnityEngine;
using System.Collections;

public class headPosition : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 c=Camera.main.transform.localPosition;
		Vector3 p = transform.localPosition;
		Transform parent = transform;
		for(int i=1;i<=8;i++){
			parent=parent.parent;
			p +=parent.localPosition;
		}
		Vector3 v=new Vector3 (c.x,p.y*2f-1.86f,-p.z*1.2f+0.28f);
		Camera.main.transform.localPosition = v;


		//8개 부모 머리 꼭대기 
		//Camera.main.transform.localPosition = new Vector3 (p.x+0.08f,p.y-0.2f,p.z+0.38f);
		//12개 부모 라이플.
		//Camera.main.transform.localPosition = new Vector3 (p.x-0.905632f,p.y+0.183f,p.z+0.340f);

	}
}

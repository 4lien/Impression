using UnityEngine;
using System.Collections;

public class ClayTarget : GameObjectParent {

	void Start () {
		hp = 10;
	}
	// Update is called once per frame
	void Update () {
		if (hp <= 0) {
			Destroy(self);	//오브젝트 삭제
			GameManager.balls.Remove(this);
		}
	}
}

using UnityEngine;
using System.Collections;

public class DestroyEffect : MonoBehaviour {

	void Start(){
		StartCoroutine(destroy());

	}
	IEnumerator destroy(){
		yield return new WaitForSeconds(2f);
		Destroy(gameObject);
	}
}

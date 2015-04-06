using System.Collections;
using UnityEngine;

/* 케릭 및 적의 기본형 */
public class GameObjectParent : MonoBehaviour {
	public float hp;
	public float damage;
	public GameObject self;
	public void hit(float Damage){
		hp -= Damage;
	}
}

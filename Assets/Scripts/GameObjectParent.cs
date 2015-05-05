using System.Collections;
using UnityEngine;

/* 케릭 및 적의 기본형 */
public class GameObjectParent : MonoBehaviour {
	public float hp=100f;
	public float damage=10f;
	public GameObject self;
	public void hit(float Damage){
		hp -= Damage;
	}
}

﻿using UnityEngine;
using System.Collections;

public class ClayTarget : GameObjectParent {

	public GameObject explosionEffect;
	public AudioClip explosionSound;
	void Start () {
		hp = 10;
	}
	// Update is called once per frame
	void Update () {
		if (hp <= 0) {
			Instantiate(explosionEffect,transform.position,transform.rotation);
			AudioSource.PlayClipAtPoint(explosionSound, transform.position,100f);
			Destroy(gameObject);	//오브젝트 삭제
			GameManager.balls.Remove(this);
		}
	}
}

﻿using UnityEngine;
using System.Collections;

public class _OSDController : MonoBehaviour {
	private bool isForDown = false;
	private bool isBackDown = false;
	private bool isLeftDown = false;
	private bool isRightDown = false;
	private bool isFireDown=false;
	GameObject player;
	void Start(){
		player=GameObject.Find("First Person Controller");
	}
	public void onForwardDown(){
		isForDown = true;
	}
	public void onForwardUP(){
		isForDown = false;
	}
	public void onBackDown(){
		isBackDown = true;
	}
	public void onBackUP(){
		isBackDown = false;
	}
	public void onLeftDown(){
		isLeftDown = true;
	}
	public void onLeftUP(){
		isLeftDown = false;
	}
	public void onRightDown(){
		isRightDown = true;
	}
	public void onRightUP(){
		isRightDown = false;
	}
	public void onFireDown(){
		isFireDown = true;
	}
	public void onFireUp(){
		isFireDown = false;
	}
	void Update(){
		player.SendMessage ("MoveForward",isForDown);
		player.SendMessage ("MoveBack",isBackDown);
		player.SendMessage ("MoveLeft",isLeftDown);
		player.SendMessage ("MoveRight",isRightDown);
		player.SendMessage ("Fire", isFireDown);
		
	}
}

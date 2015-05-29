using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof (CharacterMotor))]
public class FirstPersonController : GameObjectParent {
	public Animator Ani;
	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	public float hitRange= 5.0F;
	public float reloadTime=5.0F;
	public float waitJumpTime=2.0f;
	public int curAmmo = 30;
	public int maxAmmo= 30;
	public int remainAmmo=120;

	public AudioClip fireSnd;
	public AudioClip hitSnd;
	public AudioClip hitSnd2;
	public AudioClip jumpSnd;
	public AudioClip noAmmoSnd;
	public AudioClip reloadSnd;
	public GameObject muzzleFlash;
	public GameObject hitEffect;
	public GameObject gun;
	public GameObject muzzlePosition;
	private GameObject pcamera;
	public GameObject camPos;
	public GameObject bulletHole;
	public LayerMask ignoreRaycast;
	public Text HPtext;
	public Text AMMO;
	public ProgressBar ReloadBar;
	CharacterMotor motor;
	CharacterController con;

	public bool forward=false;
	public bool backward=false;
	public bool left=false;
	public bool right=false;
	public bool shooting=false;
	bool jumpFlag=false;
	public bool jumping=false;
	int arrayLength=30;
	const float gunSpeed = 8f;
	float fireDelay=0f;
	float reloadTimer=0f;
	float jumpTimer=0f;
	CharacterController controller;
	const float sin45 = 0.7071067811865475F;
		// Use this for initialization

	static ArrayList bulletHoles;
	HP hp;
	void Start () {
		pcamera= transform.Find("Main Camera").gameObject;
		hp = GetComponent<HP> ();
		controller = GetComponent<CharacterController>();
		bulletHoles=new ArrayList();
		motor=this.GetComponent<CharacterMotor>();
		con = GetComponent < CharacterController >();
		AMMO.text = curAmmo + "/" + remainAmmo;
	}

	RaycastHit hitRay;
	int lastBullet = 0;

	bool noammoFlag=false;
	void gunFire(){
		if (fireDelay == 0) {	//빵하고 쏨.
			if(curAmmo<=0){	//총알이 없으면
				if(noammoFlag){
					gun.audio.PlayOneShot(noAmmoSnd);
					noammoFlag=false;
				}
				return;
			}
			curAmmo--;
			if(curAmmo<=0)noammoFlag=true; 
			reloadStop();
			fireDelay += Time.deltaTime*gunSpeed;
			muzzlePosition.audio.PlayOneShot(fireSnd);
			Instantiate(muzzleFlash,muzzlePosition.transform.position,transform.rotation);
			Physics.Raycast (pcamera.transform.position, pcamera.transform.forward,out hitRay, 100,~ignoreRaycast);

			if(hitRay.transform==null)return;
			if(hitRay.transform.tag=="unstatic"){
				hitRay.transform.SendMessageUpwards("hit",damage);
				Instantiate(hitEffect,hitRay.point,transform.rotation);
				AudioSource.PlayClipAtPoint (hitSnd,transform.position);
			}
			/* 탄흔 그리기 */
			if(hitRay.transform.tag!="unstatic"&&hitRay.transform.tag!="Player"){
				if(lastBullet>=arrayLength)lastBullet=0;	//끝까지 갔으면 처음으로
				if(bulletHoles.Count<arrayLength){	//처음엔 생성만
					bulletHoles.Insert(lastBullet,(GameObject)Instantiate(bulletHole,hitRay.point+(hitRay.normal*0.01f),Quaternion.LookRotation(hitRay.normal)));
					lastBullet++;
				}else{	//생성을 다 했으면 삭제하면서 돌기시작
					Destroy((GameObject)bulletHoles[lastBullet]);	//오브젝트 삭제
					bulletHoles.RemoveAt(lastBullet);	//주소 삭제
					//생성
					bulletHoles.Insert(lastBullet,(GameObject)Instantiate(bulletHole,hitRay.point+(hitRay.normal*0.01f),Quaternion.LookRotation(hitRay.normal)));
					lastBullet++;
				}
			}
		}
		AMMO.text = curAmmo + "/" + remainAmmo;
	}



	// Update is called once per frame
	private float playTime=0f;
	void Update () {
		playTime += Time.deltaTime;
		if (hp.val <= 0) {
			HPtext.text="HP:0";
			return;
		}
		Debug.DrawRay(pcamera.transform.position, pcamera.transform.forward*100, Color.red);

		if (fireDelay > 0) {
			fireDelay-=Time.deltaTime;
		} else {
			fireDelay=0;
		}
		if(shooting)gunFire();
		Jump (jumping);
		bool isBW=false;
		Ani.SetFloat ("runDirect",0);
		switch(getDirection()){
			case -2 : {//↙
				motor.inputMoveDirection=(-transform.right-transform.forward)*sin45*0.5f;
				//controller.Move (transform.right*-1f * speed * Time.deltaTime*sin45*0.5f);
				//controller.Move (transform.forward *-1f* speed * Time.deltaTime*sin45*0.5f);
				Ani.SetFloat ("runDirect",-1);
				isBW=true;
				break;
			}case 4 : {//↘
				motor.inputMoveDirection=(transform.right-transform.forward)*sin45*0.5f;
				//controller.Move (transform.right * speed * Time.deltaTime*sin45*0.5f);
				//controller.Move (transform.forward *-1f* speed * Time.deltaTime*sin45*0.5f);
				Ani.SetFloat ("runDirect",1);
				isBW=true;
				break;
			}case -4 : {//↖
				motor.inputMoveDirection=(-transform.right+transform.forward)*sin45;
				//controller.Move (transform.right*-1f * speed * Time.deltaTime*sin45);
				//controller.Move (transform.forward * speed * Time.deltaTime*sin45);
				Ani.SetFloat ("runDirect",-1);
				break;
			}case 2 : {//↗
				motor.inputMoveDirection=(transform.right+transform.forward)*sin45;
				//controller.Move (transform.right * speed * Time.deltaTime*sin45);
				//controller.Move (transform.forward * speed * Time.deltaTime*sin45);
				Ani.SetFloat ("runDirect",1);

				break;
			}case -3 : {//←
				motor.inputMoveDirection=-transform.right*0.5f;
				//controller.Move (transform.right*-1f * speed * Time.deltaTime*0.5f);
				Ani.SetFloat ("runDirect",-1);
				break;
			}case 3 : {//→
				motor.inputMoveDirection=transform.right*0.5f;
				//controller.Move (transform.right * speed * Time.deltaTime*0.5f);
				Ani.SetFloat ("runDirect",1);
				break;
			}case 1 : {//↓
				motor.inputMoveDirection=-transform.forward*0.5f;
				//controller.Move (transform.forward *-1f* speed *0.5f* Time.deltaTime);
				isBW=true;
				break;
			}case -1 : {//↑
				motor.inputMoveDirection=transform.forward;
				//controller.Move (transform.forward * speed * Time.deltaTime);
				break;
			}
			default:{
				motor.inputMoveDirection=Vector3.zero;
				break;
			}
		}

		if (!isBW) {
			Ani.SetFloat ("speed", controller.velocity.magnitude);

		} else {
			Ani.SetFloat ("speed", -controller.velocity.magnitude);
		}

		Vector3 degree = pcamera.transform.localRotation.eulerAngles;

		if (degree.x > 0 && degree.x <= 90) {
			Ani.SetFloat ("lookDegree",-degree.x);
		}
		if (degree.x < 360 && degree.x >= 270) {
			Ani.SetFloat ("lookDegree",360-degree.x);
		}

	}
	public void hit(float Damage){
		hp.val -= Damage;
		HPtext.text = "HP:" + (int)hp.val;
		AudioSource.PlayClipAtPoint(hitSnd2,transform.position,0.2f);
		if (hp.val <= 0)
			dead ();
	}
	private float score = 0f;
	private int killed=0;
	public void enemyKill(){
		killed++;
		float addscore = (180f - playTime)/180f*300f;
		if (addscore <= 20)
			addscore = 20;
		score += addscore;
	}
	public int getScore(){
		return (int)Mathf.Round (score);
	}
	public int getKilled(){
		return killed;
	}

	void dead(){
		Ani.SetBool ("dead",true);
		con.stepOffset = 0f;
		con.enabled = false;
		motor.enabled = false;
		StartCoroutine (destroy());
	}

	IEnumerator destroy(){
		yield return new WaitForSeconds(1.5f);
		pcamera.transform.parent = camPos.transform;	//카메라 이동
		StartCoroutine (reset());
	}
	IEnumerator reset(){
		yield return new WaitForSeconds(5f);
		Application.LoadLevel (Application.loadedLevelName);
	}

	int getDirection(){
		int temp = 0;
		temp += forward ? -1 : 0;
		temp += backward ? 1 : 0;
		temp += left ? -3 : 0;
		temp += right ? 3 : 0;
		return temp;
	}
	public void MoveForward(bool val){
		forward = val;
	}
	public void MoveBack(bool val){
		backward = val;
	}
	public void MoveLeft(bool val){
		left = val;
	}
	public void MoveRight(bool val){
		right = val;
	}
	public void Fire(bool val)	{
		shooting=val;
	}
	public void reloadStart(){
	}
	public void reloadStop(){
		ReloadBar.percent=0f;
		ReloadBar.off ();
		reloadTimer=0f;
		gun.audio.Stop();
		reloadSndFlag=true;
	} 
	public void reload(){
		reloadStop ();

		int needAmmo = maxAmmo - curAmmo;
		if (remainAmmo >= needAmmo) {
			curAmmo += needAmmo;
			remainAmmo -= needAmmo;
		} else {
			curAmmo+=remainAmmo;
			remainAmmo=0;
		}
		noammoFlag = false;

		AMMO.text = curAmmo + "/" + remainAmmo;
	}
	bool reloadSndFlag=true;
	public void Jump(bool val){
		if (hp.val <= 0)
			return;
		if (val) {	//다운
			jumpTimer+=Time.deltaTime;
			if(remainAmmo>0&&curAmmo<maxAmmo){
				ReloadBar.on (); //리로드바 키고
				if(reloadSndFlag){
					gun.audio.PlayOneShot(reloadSnd);
					reloadSndFlag=false;
				}

				//리로드 안된 상태면 리로드 시작
				if(reloadTimer<reloadTime){
					reloadTimer+=Time.deltaTime;
					ReloadBar.percent=reloadTimer/reloadTime;
				}else{
					reloadTimer=reloadTime;
					reload ();
				}
			}
			motor.inputJump = false;
		}
		if(jumpFlag&&!val){	//업
			//누른지 일정 시간이 안지났을때
			if(jumpTimer<=waitJumpTime){
				if(Physics.Raycast(transform.position, -Vector3.up,0.01f)){	//땅에 붙어있을때만
					audio.PlayOneShot(jumpSnd);	//기합
				}
				motor.inputJump = true; //점프
			}
			reloadStop ();
			jumpTimer=0f;
		}
		jumpFlag = val;
	}


}

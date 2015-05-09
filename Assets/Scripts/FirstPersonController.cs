using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterMotor))]
public class FirstPersonController : GameObjectParent {
	public Animator Ani;
	private Vector3 moveDirectionhitRayVector3.zero;
	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	public float hitRange= 5F;
	public AudioClip fireSnd;
	public AudioClip hitSnd;
	public AudioClip hitSnd2;
	public AudioClip jumpSnd;
	public GameObject muzzleFlash;
	public GameObject hitEffect;
	public GameObject gun;
	public GameObject muzzlePosition;
	public GameObject camera;
	public GameObject bulletHole;
	public LayerMask ignoreRaycast;
	CharacterMotor motor;
	CharacterController con;

	bool forward=false;
	bool backward=false;
	bool left=false;
	bool right=false;
	bool shooting=false;
	bool jump=false;
	int arrayLength=30;
	const float gunSpeed = 8f;
	float fireDelay=0f;
	CharacterController controller;
	const float sin45 = 0.7071067811865475F;
		// Use this for initialization

	static ArrayList bulletHoles;
	void Start () {
		//bulletHoleMask = ~bulletHoleMask;
		controller = GetComponent<CharacterController>();
		bulletHoles=new ArrayList();
		motor=this.GetComponent<CharacterMotor>();
		damage = 80;
		hp = 100;
		con = GetComponent < CharacterController >();
	}



	RaycastHit hitRay;
	//RaycastHit hit2;
	int lastBullet = 0;

	void gunFire(){
		if (fireDelay == 0) {	//빵하고 쏨.
			fireDelay += Time.deltaTime*gunSpeed;
			AudioSource.PlayClipAtPoint (fireSnd, muzzlePosition.transform.position);
			Instantiate(muzzleFlash,muzzlePosition.transform.position,transform.rotation);
			Physics.Raycast (camera.transform.position, camera.transform.forward,out hitRay, 100,~ignoreRaycast);
			//Physics.SphereCast(camera.transform.position,hitRange, camera.transform.forward,out hit2, 100,dynamicLayer);

			//if(hit2.transform!=null)
			//	hit2.transform.SendMessageUpwards("hit",damage);
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
	}



	// Update is called once per frame
	void Update () {
		Debug.DrawRay(camera.transform.position, camera.transform.forward*100, Color.red);

		if (fireDelay > 0) {
			fireDelay-=Time.deltaTime;
		} else {
			fireDelay=0;
		}
		if(shooting)gunFire();
		bool isBW=false;
		float direct = 0f;
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
		}

		if (!isBW) {
			Ani.SetFloat ("speed", controller.velocity.magnitude);

		} else {
			Ani.SetFloat ("speed", -controller.velocity.magnitude);
		}

		Vector3 degree = camera.transform.localRotation.eulerAngles;

		if (degree.x > 0 && degree.x <= 90) {
			Ani.SetFloat ("lookDegree",-degree.x);
		}
		if (degree.x < 360 && degree.x >= 270) {
			Ani.SetFloat ("lookDegree",360-degree.x);
		}

	}
	public void hit(float Damage){
		hp -= Damage;
		AudioSource.PlayClipAtPoint(hitSnd2,transform.position,0.2f);
		if (hp < 0)
			dead ();
	}
	void dead(){
		Ani.SetBool ("dead",true);
		StartCoroutine (destroy());
	}

	IEnumerator destroy(){
		yield return new WaitForSeconds(2);
		con.stepOffset = 0f;
		con.enabled = false;
		motor.enabled = false;
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
	public void Jump(bool val){
		if (val)	//다운
			motor.inputJump = false;
		if(jump&&!val){	//업
			if(Physics.Raycast(transform.position, -Vector3.up,0.01f)){	//땅에 붙어있을때만
				AudioSource.PlayClipAtPoint(jumpSnd,transform.position);	//기합
			}
			motor.inputJump = true;
		}
		jump = val;
	}


}

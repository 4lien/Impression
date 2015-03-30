using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterMotor))]
public class FirstPersonController : MonoBehaviour {
	public Animator Ani;
	private Vector3 moveDirection = Vector3.zero;
	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	public Transform me;
	public AudioClip fireSnd;
	public GameObject muzzleFlash;
	public GameObject gun;
	public GameObject muzzlePosition;
	public GameObject camera;
	public GameObject bulletHole;
	public LayerMask bulletHoleMask;
	CharacterMotor motor;

	bool forward=false;
	bool backward=false;
	bool left=false;
	bool right=false;
	bool shooting=false;
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

	}



	RaycastHit hit;

	int lastBullet = 0;

	void gunFire(){
		if (fireDelay == 0) {
			fireDelay += Time.deltaTime*gunSpeed;
			AudioSource.PlayClipAtPoint (fireSnd, muzzlePosition.transform.position);
			Instantiate(muzzleFlash,muzzlePosition.transform.position,transform.rotation);
			Physics.Raycast (camera.transform.position, camera.transform.forward,out hit, 100);
			if(hit.transform==null)return;
			/* 탄흔 그리기 */
			if(hit.transform.tag!="unstatic"&&hit.transform.tag!="Player"){
				if(lastBullet>=arrayLength)lastBullet=0;	//끝까지 갔으면 처음으로
				if(bulletHoles.Count<arrayLength){	//처음엔 생성만
					bulletHoles.Insert(lastBullet,(GameObject)Instantiate(bulletHole,hit.point+(hit.normal*0.01f),Quaternion.LookRotation(hit.normal)));
					lastBullet++;
				}else{	//생성을 다 했으면 삭제하면서 돌기시작
					Destroy((GameObject)bulletHoles[lastBullet]);	//오브젝트 삭제
					bulletHoles.RemoveAt(lastBullet);	//주소 삭제
					//생성
					bulletHoles.Insert(lastBullet,(GameObject)Instantiate(bulletHole,hit.point+(hit.normal*0.01f),Quaternion.LookRotation(hit.normal)));
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
		//오리 Y=1.6 Z=0.27
		//뜀 Y=1.486 Z=0.297
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

		Vector3 degree = Camera.main.transform.localRotation.eulerAngles;

		if (degree.x > 0 && degree.x <= 90) {
			Ani.SetFloat ("lookDegree",-degree.x);
		}
		if (degree.x < 360 && degree.x >= 270) {
			Ani.SetFloat ("lookDegree",360-degree.x);
		}

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

}

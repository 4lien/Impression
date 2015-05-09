using UnityEngine;
using System.Collections;

//[RequireComponent (typeof (CharacterMotor))]
public class EnemyAI : GameObjectParent {
	public Animator Ani;
	private Vector3 moveDirection = Vector3.zero;
	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	public float hitRange= 5F;
	public AudioClip fireSnd;
	public GameObject muzzleFlash;
	public GameObject gun;
	public GameObject muzzlePosition;
	public GameObject hitEffect;
	public GameObject camera;
	public GameObject bulletHole;
	public LayerMask ignoreRaycast;
	public Transform[] wayPoints;
	//CharacterMotor motor;
	
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
//		motor=this.GetComponent<CharacterMotor>();
		player = GameObject.FindGameObjectWithTag("Player");
		nav = GetComponent < NavMeshAgent> ();
		col = GetComponent<SphereCollider> ();
	}
	



	/* AI구현 부분 시작 */
	//private EnemySight enemySight;
	private NavMeshAgent nav;
	private GameObject player;
	//private LastPlayerSighting lastPlayerSighting;
	private float chaseTimer;
	private float patrolTimer;
	private int wayPointIndex;
	private SphereCollider col;
	private float fieldOfViewAngle=110f;
	private int wpIndex=0;
	private float waitTimer=0;
	public float waitTime=0.0001f;
	bool playerInSight=false;
	void OnTriggerStay(Collider other) {
		if(player.transform!=other.transform)
			return;
		Vector3 direction = other.transform.position - transform.position;
		float angle = Vector3.Angle (direction, transform.forward);
		if (angle < fieldOfViewAngle * 0.5f) {	//시야 범위 안에 있을때
			nav.Stop ();
			RaycastHit hit;
			if(Physics.Raycast(camera.transform.position,direction.normalized,out hit,col.radius,~ignoreRaycast)){	//보이면
				if(hit.collider.gameObject==other.gameObject){
					Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x+Random.Range(-3f,3f),0,direction.z)); //위아래 제외
					transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);	
					targetRotation = Quaternion.LookRotation(direction);	//위아래 빼고 몸통만
					Vector3 targetPos=other.transform.position;
					targetPos.y+=1.4f+Random.Range(-0.1f,0.1f); //플레이어 크기가 2f 즉 1.4부분을 조준
					camera.transform.LookAt(targetPos);	//타겟을 보게 한다음에
					Vector3 temp=camera.transform.localEulerAngles;
					temp.y=0;	//y랑 z를 0으로
					temp.z=0;
					camera.transform.localEulerAngles=temp;	//y랑 z만 0으로
					gunFire();
				}
			}
		}
	}

	/* AI구현 부분 끝 */





	
	RaycastHit hit;
	int lastBullet = 0;
	
	void gunFire(){
		if (fireDelay == 0) {	//빵하고 쏨.
			fireDelay += Time.deltaTime*gunSpeed;
			AudioSource.PlayClipAtPoint (fireSnd, muzzlePosition.transform.position);
			Instantiate(muzzleFlash,muzzlePosition.transform.position,transform.rotation);
			if(Physics.Raycast(camera.transform.position,camera.transform.forward,out hit,col.radius)){
				hit.transform.SendMessageUpwards("hit",damage);
				Instantiate(hitEffect,hit.point,transform.rotation);
			}
		}
	}
	
	
	void temp(){

	}
	// Update is called once per frame
	void Update () {

		Debug.DrawRay(camera.transform.position, camera.transform.forward*100, Color.red);
		if (fireDelay > 0) {
			fireDelay-=Time.deltaTime;
		} else {
			fireDelay=0;
		}


		nav.SetDestination (wayPoints [wpIndex].position);
		if (nav.remainingDistance < nav.stoppingDistance) {	//도착하였으면
			waitTimer+=Time.deltaTime;	//기다림
			if(waitTimer>waitTime){	//다 기다렸으면
				waitTimer=0f;
				wpIndex++;
				wpIndex=wpIndex%wayPoints.Length;	//인덱스 증가
				nav.SetDestination (wayPoints [wpIndex].position);
			}

		}
		Ani.SetFloat ("speed", nav.velocity.magnitude);


			/*

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
		
		Vector3 degree = camera.transform.localRotation.eulerAngles;
		
		if (degree.x > 0 && degree.x <= 90) {
			Ani.SetFloat ("lookDegree",-degree.x);
		}
		if (degree.x < 360 && degree.x >= 270) {
			Ani.SetFloat ("lookDegree",360-degree.x);
		}*/
		
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

﻿using UnityEngine;
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
	public bool navON=true;
	CapsuleCollider colider;
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
		bulletHoles=new ArrayList();
		player = GameObject.FindGameObjectWithTag("Player");
		col = GetComponent<SphereCollider> ();
		colider = GetComponent<CapsuleCollider> ();
		if (navON) {
			nav = GetComponent < NavMeshAgent> ();
		}
	}
	



	/* AI구현 부분 시작 */
	//private EnemySight enemySight;
	private NavMeshAgent nav;
	private GameObject player;
	//private LastPlayerSighting lastPlayerSighting;
	private float patrolTimer;
	private int wayPointIndex;
	private SphereCollider col;
	public float fieldOfViewAngle=110f;
	private int wpIndex=0;
	private float waitTimer=0;
	public float waitTime=0.0001f;
	bool sawPlayer=false;
	void OnTriggerStay(Collider other) {	//범위안에 플레이어 포착
		if(player.transform!=other.transform||hp<=0)	//플레이어가 아니거나 죽었으면 안함
			return;
		Vector3 direction = other.transform.position - transform.position;
		float angle = Vector3.Angle (direction, transform.forward);
		if (angle < fieldOfViewAngle * 0.5f) {	//시야 범위 안에 있을때
			RaycastHit hit;
			if (Physics.Raycast (camera.transform.position, direction.normalized, out hit, col.radius, ~ignoreRaycast)) {	//보이면
				if (hit.collider.gameObject == other.gameObject) {
					nav.Stop ();
					chaseTimer=0f;
					sawPlayer=true;
					Quaternion targetRotation = Quaternion.LookRotation (new Vector3 (direction.x + Random.Range (-3f, 3f), 0, direction.z)); //위아래 제외
					transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, 5f * Time.deltaTime);	
					targetRotation = Quaternion.LookRotation (direction);	//위아래 빼고 몸통만
					Vector3 targetPos = other.transform.position;
					targetPos.y += 1.4f + Random.Range (-0.1f, 0.1f); //플레이어 크기가 2f 즉 1.4부분을 조준
					camera.transform.LookAt (targetPos);	//타겟을 보게 한다음에
					Vector3 temp = camera.transform.localEulerAngles;
					temp.y = 0;	//y랑 z를 0으로
					temp.z = 0;
					camera.transform.localEulerAngles = temp;	//y랑 z만 0으로
					gunFire ();
				}
			}
		}
	}
	public void hit(float Damage){
		hp -= Damage;
		if (hp < 0)
			dead ();
	}
	void dead(){
		Ani.SetBool ("dead",true);
		nav.enabled = false;
		col.enabled = false;
		StartCoroutine (destroy());
	}
	
	IEnumerator destroy(){
		yield return new WaitForSeconds(1.5f);
		transform.rigidbody.useGravity = false;
		colider.enabled = false;
		this.enabled = false;
	}
	/* AI구현 부분 끝 */
	RaycastHit hitRay;
	void gunFire(){
		if (fireDelay == 0) {	//빵하고 쏨.
			fireDelay += Time.deltaTime*gunSpeed;
			AudioSource.PlayClipAtPoint (fireSnd, muzzlePosition.transform.position);
			Instantiate(muzzleFlash,muzzlePosition.transform.position,transform.rotation);
			if(Physics.Raycast(camera.transform.position,camera.transform.forward,out hitRay,col.radius)){
				hitRay.transform.SendMessageUpwards("hit",damage);
				Instantiate(hitEffect,hitRay.point,transform.rotation);
			}
		}
	}
	
	// Update is called once per frame
	bool arrived=false;
	float chaseTimer;
	public float chaseTime=3f;	//해당 시간만큼 플레이어 쫒아감.
	void Update () {
		if (hp <= 0)
			return;
		Debug.DrawRay(camera.transform.position, camera.transform.forward*100, Color.red);
		if (fireDelay > 0) {
			fireDelay-=Time.deltaTime;
		} else {
			fireDelay=0;
		}
		if (!navON)
			return;
		if (sawPlayer) {	//플레이어를 봤으면 추격 시작
			chaseTimer+=Time.deltaTime;
			nav.SetDestination (player.transform.position);
		}
		if (chaseTimer > chaseTime) {	//추격 시간 지났을경우
			sawPlayer=false;	//못본걸로
			chaseTimer=0f;
		}

		if (!sawPlayer) {	//플레이어를 보지 못했으면.
			if (nav.remainingDistance < nav.stoppingDistance) {	//도착하였으면
				waitTimer += Time.deltaTime;	//기다림
				if (waitTimer > waitTime) {	//다 기다렸으면
					waitTimer = 0f;
					wpIndex++;
					wpIndex = wpIndex % wayPoints.Length;	//인덱스 증가
					nav.SetDestination (wayPoints [wpIndex].position);

				}
			}else{	//도중이면
				nav.SetDestination (wayPoints [wpIndex].position);
			}
		}
		Ani.SetFloat ("speed", nav.velocity.magnitude);
	}

	
}

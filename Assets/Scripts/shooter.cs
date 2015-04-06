using UnityEngine;
using System.Collections;

public class shooter : MonoBehaviour {
	public GameObject ball;
	float fireDelay=0f;
	const float gunSpeed = 40f;
	int arrayLength=30;
	// Use this for initialization
	void Start () {
		GameManager.balls = new ArrayList ();
	}

	int lastBall = 0;

	void fire(){
		if (fireDelay > 0)return;
		fireDelay += Time.deltaTime*gunSpeed;
		if(lastBall>=arrayLength)lastBall=0;	//끝까지 갔으면 처음으로
		GameObject instBall=(GameObject)Instantiate(ball,transform.position,transform.rotation);
		instBall.rigidbody.AddForce (transform.right*Random.Range(240f,260f));
		instBall.rigidbody.AddForce (transform.right*Random.Range(-50f,50f));
		instBall.rigidbody.AddForce (transform.up*Random.Range(-50f,50f));
		instBall.rigidbody.AddForce (transform.up*200f);
		if(GameManager.balls.Count<arrayLength){	//처음엔 생성만
			GameManager.balls.Insert(lastBall,instBall);
			lastBall++;
		}else{	//생성을 다 했으면 삭제하면서 돌기시작
			Destroy((GameObject)GameManager.balls[lastBall]);	//오브젝트 삭제
			GameManager.balls.RemoveAt(lastBall);	//주소 삭제
			//생성
			GameManager.balls.Insert(lastBall,instBall);
			lastBall++;
		}
	}
	// Update is called once per frame
	void Update () {
		if (fireDelay > 0) {
			fireDelay-=Time.deltaTime;
		} else {
			fireDelay=0;
		}
		fire ();
		//Instantiate (ball, transform.position, transform.rotation * Random.rotation);
	}
}

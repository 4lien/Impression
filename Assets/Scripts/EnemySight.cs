using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour {
	public float fieldOfViewAngle=110f;
	public bool playerInSight;
	public Vector3 personalLastSighting;
	private NavMeshAgent nav;
	private SphereCollider col;
	private GameObject player;

	void Start () {
		player=GameObject.FindGameObjectWithTag("Player");
		nav = GetComponent < NavMeshAgent> ();
		col = GetComponent<SphereCollider> ();

	}
	
	void Update () {

	}


}

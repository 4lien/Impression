using UnityEngine;
using System.Collections;

public class myNetworkPlayer : Photon.MonoBehaviour
{
	private Vector3 correctPlayerPos = Vector3.zero; // We lerp towards this
	private Quaternion correctPlayerRot = Quaternion.identity; // We lerp towards this
	private float runDirect, speed, lookDegree;
	private bool dead;
	GameObject player;
	Animator Ani;
	void Start(){
		player = transform.FindChild ("Player").gameObject;
		Ani = player.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update()
	{
		if (!photonView.isMine)
		{
			player.transform.position = Vector3.Lerp(player.transform.position, this.correctPlayerPos, 0.5f);
			player.transform.rotation = Quaternion.Lerp(player.transform.rotation, this.correctPlayerRot, 0.5f);
			Ani.SetFloat ("runDirect",Mathf.Lerp(Ani.GetFloat("runDirect"),runDirect,0.5f));
			Ani.SetFloat ("speed",Mathf.Lerp(Ani.GetFloat("speed"),speed,0.5f));
			Ani.SetFloat ("lookDegree",Mathf.Lerp(Ani.GetFloat("lookDegree"),lookDegree,0.5f));
			Ani.SetBool ("dead",dead);
		}
	}
	
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			// We own this player: send the others our data
			stream.SendNext(player.transform.position);
			stream.SendNext(player.transform.rotation);
			stream.SendNext(Ani.GetFloat("runDirect"));
			stream.SendNext(Ani.GetFloat("speed"));
			stream.SendNext(Ani.GetFloat("lookDegree"));
			stream.SendNext(Ani.GetBool("dead"));
			
			/*FirstPersonController myC = transform.Find("Player").gameObject.GetComponent<FirstPersonController>();
			stream.SendNext((int)myC._characterState);*/
		}
		else
		{
			// Network player, receive data
			this.correctPlayerPos = (Vector3)stream.ReceiveNext();
			this.correctPlayerRot = (Quaternion)stream.ReceiveNext();
			runDirect=(float)stream.ReceiveNext();
			speed=(float)stream.ReceiveNext();
			lookDegree=(float)stream.ReceiveNext();
			dead=(bool)stream.ReceiveNext();
			/*myThirdPersonController myC = GetComponent<myThirdPersonController>();
			myC._characterState = (CharacterState)stream.ReceiveNext();*/
		}
	}
}

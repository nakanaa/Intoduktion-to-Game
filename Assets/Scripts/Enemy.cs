using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class Enemy : Photon.MonoBehaviour {

	[Header ("Enemy Stats")]
	public int Health;
	public float MovementSpeed;

	public GameObject CurrentSpell;

	[Header ("Enemy Behaviour")]
	public float DistanceToAwake;
	public float DistanceToAttack;
	public float AttackLoopTime;
	private float lastAttackLoopTime;

	private GameObject target;
	private Animator anim;

	private Vector3 networkPos;

	void Start()
	{
		networkPos = transform.position;

		anim = GetComponent<Animator> ();
	}
	
	void Update () 
	{
		if (photonView.isMine) 
		{
			AILoop ();
		} 
		else
		{
			transform.position = Vector3.Lerp (transform.position, networkPos, NetworkController.NetworkLerp * Time.deltaTime);
		}
	}

	private void AILoop(){

		FindClosestTarget ();

		if (Health <= 0) 
		{
			photonView.RPC ("Death", PhotonTargets.AllBuffered);
		} 
		else
		{			
			if (target != null) 
			{
				if (Vector2.Distance (target.transform.position, transform.position) < DistanceToAttack) 
				{
					if ((Time.time - lastAttackLoopTime) > AttackLoopTime) 
					{
						MoveToTarget ();
						photonView.RPC ("Attack", PhotonTargets.All);
						lastAttackLoopTime = Time.time;
					}
				} 
				else
				{
					MoveToTarget ();
				}
			} 
		}
	}

	private void FindClosestTarget()
	{
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject closest in players) {
			if (target == null) {
				target = closest;
			}else if(Vector2.Distance(target.transform.position,transform.position) > Vector2.Distance(transform.position,closest.transform.position)){
				target = closest;
			}
		}
	}

	private void MoveToTarget()
	{
		if (target != null) {			
			transform.position = Vector2.MoveTowards (new Vector2 (transform.position.x, transform.position.y), new Vector2 (target.transform.position.x, target.transform.position.y), MovementSpeed * Time.deltaTime);
			float side = transform.position.x - target.transform.position.x;

			if (side < 0) {
				transform.eulerAngles = new Vector3 (0, 0, 0);
			} else {
				transform.eulerAngles = new Vector3 (0, 180, 0);
			}
			anim.SetBool ("Run", true);
		} else {
			anim.SetBool ("Run", false);
		}
	}

	[PunRPC]
	public void Attack()
	{
		anim.SetTrigger ("Attack");
		CurrentSpell.GetComponent<Spellcast> ().CastSpell ();
	}

	[PunRPC]
	public void Death()
	{
		anim.SetTrigger ("Death");
	}

	public void TakeDamage(object args)
	{
		object[] o = (object[])args;
		Health -= (int)o[0];
		GetComponent<Rigidbody2D> ().AddForce ((Vector2)o[1]*-(int)o[0],ForceMode2D.Impulse);
	}

	public void Destroy(){
		Destroy (gameObject);
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext (transform.position);
			stream.SendNext (transform.rotation);
			stream.SendNext (anim.GetBool("Run"));
		} 
		else 
		{
			networkPos = (Vector3)stream.ReceiveNext ();
			transform.rotation = (Quaternion)stream.ReceiveNext ();
			anim.SetBool ("Run", (bool)stream.ReceiveNext ());
		}
	}
}

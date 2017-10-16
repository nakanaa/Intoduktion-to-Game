using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	[Header ("Enemy Stats")]
	public int Health;
	public float MovementSpeed;

	public GameObject CurrentSpell;

	[Header ("Enemy Behaviour")]
	public float DistanceToAwake;
	public float DistanceToAttack;

	public GameObject OnHitEffect;

	private GameObject target;
	private Animator anim;

	void Start(){
		anim = GetComponent<Animator> ();
	}
	
	void Update () {
		if (Health <= 0) {
			BeforeDeath ();
		} else {
			EnemyBehaviour ();
		}
	}



	private void EnemyBehaviour(){
		FindClosestTarget ();
		MoveToTarget ();

		if (Vector2.Distance (target.transform.position, transform.position) < DistanceToAttack) {
			anim.SetTrigger ("Attack");
		}

	}


	public void Attack(){
		CurrentSpell.GetComponent<Spellcast> ().CastSpell ();
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

	private void BeforeDeath()
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
}

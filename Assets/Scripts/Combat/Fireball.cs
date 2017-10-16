using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {

	public int Damage;
	public float Speed;
	public float Duration;

	public GameObject OnHitEffect;

	private float startTime;

	void OnEnable()
	{
		startTime = Time.time;
	}
	
	void FixedUpdate() 
	{
		transform.Translate (Vector2.right * Time.deltaTime* Speed);

		if (Time.time - startTime > Duration)
		{
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D coll)
	{	
		object[] o = new object[2];
		o[0] = Damage;
		o[1] = new Vector2(transform.position.x - coll.transform.position.x,transform.position.y - coll.transform.position.y);
		coll.gameObject.SendMessage ("TakeDamage", o);

		Destroy (gameObject);

		Vector2 collisionPosition = new Vector2 (coll.transform.position.x, coll.transform.position.y) + coll.offset;

		Instantiate ((Object)OnHitEffect,collisionPosition, Quaternion.identity, null);

	}
}

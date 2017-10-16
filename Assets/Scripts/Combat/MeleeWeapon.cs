using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour {

	[Header ("Weapon Stats")]
	public int Damage;

	public GameObject OnHitEffect;


	void OnTriggerEnter2D(Collider2D coll){
	

		object[] o = new object[2];
		o[0] = Damage;
		o[1] = new Vector2(transform.position.x - coll.transform.position.x,transform.position.y - coll.transform.position.y);
		coll.gameObject.SendMessage ("TakeDamage", o);

		Vector2 collisionPosition = new Vector2 (coll.transform.position.x, coll.transform.position.y) + coll.offset;

		Instantiate ((Object)OnHitEffect,collisionPosition, Quaternion.identity, null);
	}
}

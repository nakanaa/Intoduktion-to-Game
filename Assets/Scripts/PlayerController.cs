using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[Header ("Player Stats")]
	public int Health;
	public float MovementSpeed;
	public float JumpForce;

	public float gravityScale;

	[Header ("Player Weapons")]
	public GameObject CurrentWeapon;
	public GameObject CurrentSpell;

	private Animator anim;
	private Rigidbody2D rb2d;

	private bool freezeMovement;

	private const float inputTreshold = 0.1f;

	void Start ()
	{
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
	}	

	void Update ()
	{
		rb2d.gravityScale = gravityScale;

		UserInput ();
	}

	private void UserInput()
	{
		
		// Movement //

		if (!freezeMovement)
		{
			rb2d.velocity = new Vector2 (Input.GetAxis ("Horizontal") * MovementSpeed, rb2d.velocity.y);		

			if (Input.GetAxis ("Horizontal") > inputTreshold) 
			{
				anim.SetBool ("Run", true);
				transform.localEulerAngles = new Vector3 (0, 0, 0);
			} 
			else if (Input.GetAxis ("Horizontal") < -inputTreshold) 
			{
				anim.SetBool ("Run", true);
				transform.localEulerAngles = new Vector3 (0, 180, 0);
			} 
			else 
			{
				anim.SetBool ("Run", false);
			}

			if (Input.GetButtonDown ("Jump")) 
			{
				rb2d.velocity = new Vector2 (Input.GetAxis ("Horizontal") * MovementSpeed, JumpForce);
			}
		}

		// Interactions //

		if (Input.GetButtonDown ("Melee")) 
		{
			anim.SetTrigger ("MeleeAttack");
		} 
		else if (Input.GetButtonDown ("Cast")) 
		{
			anim.SetTrigger ("Cast");
		} 
		else if (Input.GetButton ("Block")) 
		{
			Block (true);
		} 
		else 
		{
			Block (false);
		}
	}

	private void Cast()
	{
		CurrentSpell.GetComponent<Spellcast> ().CastSpell ();
	}

	private void Block(bool block)
	{
		freezeMovement = block;
		anim.SetBool ("Block",block);
	}
		
	private void TakeDamage(object args)
	{
		object[] o = (object[])args;
		Health -= (int)o[0];
		//Instantiate ((Object)OnHitEffect,transform.position, Quaternion.identity, null);
		GetComponent<Rigidbody2D> ().AddForce ((Vector2)o[1]*-(int)o[0],ForceMode2D.Impulse);
	}
}

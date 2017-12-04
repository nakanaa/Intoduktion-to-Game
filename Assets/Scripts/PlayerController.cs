using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class PlayerController : Photon.MonoBehaviour {

	public GameObject PlayerCamera;

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
	private bool isGrounded = true;

	private Vector3 networkPos;

	void Start ()
	{
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();

		if (photonView.isMine)
		{	
			GameObject camera = Instantiate (PlayerCamera);
			camera.GetComponent<DeadzoneCamera> ().target = gameObject.GetComponent<Renderer> ();
		} 
		else
		{
			GetComponent<Rigidbody2D> ().isKinematic = true;
		}
	}	

	void Update ()
	{
		if (photonView.isMine) {
			UserInput ();
		} else {
			transform.position = Vector3.Lerp (transform.position, networkPos, NetworkController.NetworkLerp * Time.deltaTime);
		}
	}

	private void UserInput()
	{		
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
				if (isGrounded) {
					rb2d.velocity = new Vector2 (Input.GetAxis ("Horizontal") * MovementSpeed, JumpForce);
					isGrounded = false;
				}
			}
		}

		if (Input.GetButtonDown ("Melee")) 
		{
			photonView.RPC ("MeleeAttack", PhotonTargets.All);
		} 
		else if (Input.GetButtonDown ("Cast")) 
		{
			photonView.RPC ("Cast", PhotonTargets.All);
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

	[PunRPC]
	public void MeleeAttack()
	{
		anim.SetTrigger ("MeleeAttack");
	}

	[PunRPC]
	public void Cast()
	{
		anim.SetTrigger ("Cast");
		CurrentSpell.GetComponent<Spellcast> ().CastSpell ();
	}

	public void Block(bool block)
	{
		freezeMovement = block;
		anim.SetBool ("Block",block);
	}
		
	private void TakeDamage(object args)
	{
		object[] o = (object[])args;
		Health -= (int)o[0];
		GetComponent<Rigidbody2D> ().AddForce ((Vector2)o[1]*-(int)o[0],ForceMode2D.Impulse);
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		isGrounded = true;
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext (transform.position);
			stream.SendNext (transform.rotation);
			stream.SendNext (anim.GetBool("Run"));
			stream.SendNext (anim.GetBool("Block"));
		} 
		else 
		{
			networkPos = (Vector3)stream.ReceiveNext ();
			transform.rotation = (Quaternion)stream.ReceiveNext ();
			anim.SetBool ("Run", (bool)stream.ReceiveNext ());
			anim.SetBool ("Block", (bool)stream.ReceiveNext ());
		}
	}
}

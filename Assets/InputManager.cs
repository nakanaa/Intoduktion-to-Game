using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
	
	public GameObject AndroidCanvas;

	public static float Horizontal;
	public static float Vertical;

	public static bool MeleeButton;

	public static bool JumpButton;

	public static bool CastButton;

	public static bool BlockButton;

	void Start()
	{
		#if UNITY_STANDALONE_WIN || UNITY_EDITOR
		AndroidCanvas.SetActive(false);
		#endif
	}

	void Update () {

		#if UNITY_STANDALONE_WIN || UNITY_EDITOR

		Horizontal = Input.GetAxis ("Horizontal");
		Vertical = Input.GetAxis ("Vertical");

		MeleeButton = Input.GetButtonDown ("Melee");

		JumpButton = Input.GetButtonDown ("Jump");


		CastButton = Input.GetButtonDown ("Cast");

		BlockButton = Input.GetButton ("Block");

		#endif

	}

	public void SetHorizontal(float value)
	{
		Horizontal = value;
	}

	public void SetVertical(float value)
	{
		Vertical = value;
	}

	public void SetMeleeButton(bool value){
		MeleeButton = value;
	}

	public void SetJumpButton(bool value){
		JumpButton = value;
	}

	public void SetCastButton(bool value){
		CastButton = value;
	}

	public void SetBlockButton(bool value){
		BlockButton = value;
	}
}

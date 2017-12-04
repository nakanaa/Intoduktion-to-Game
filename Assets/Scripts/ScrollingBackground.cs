using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {

	public Transform RigthObject;
	public Transform MiddleObject;
	public Transform LeftObject;

	public float MoveAmount;
	public float ParalaxSpeed;
	public float AdjustMoveUpdate;

	private float deltaX;
	private float lastCameraX;


	void FixedUpdate(){

		if (Camera.main != null) 
		{
			deltaX = Camera.main.transform.position.x - lastCameraX;
			transform.position += Vector3.right * (deltaX * ParalaxSpeed);
			lastCameraX = Camera.main.transform.position.x;


			if (LeftObject.position.x > Camera.main.transform.position.x - AdjustMoveUpdate) {
				ScrollLeft ();
			} else if (RigthObject.position.x < Camera.main.transform.position.x + AdjustMoveUpdate) {
				ScrollRight ();
			}
		}
	}

	private void ScrollLeft(){
		MiddleObject.localPosition = new Vector3 (MiddleObject.localPosition.x - MoveAmount, MiddleObject.localPosition.y);
		RigthObject.localPosition = new Vector3 (RigthObject.localPosition.x - MoveAmount, RigthObject.localPosition.y);
		LeftObject.localPosition = new Vector3 (LeftObject.localPosition.x - MoveAmount, LeftObject.localPosition.y);

	}

	private void ScrollRight(){
		MiddleObject.localPosition = new Vector3 (MiddleObject.localPosition.x + MoveAmount, MiddleObject.localPosition.y);
		RigthObject.localPosition = new Vector3 (RigthObject.localPosition.x + MoveAmount, RigthObject.localPosition.y);
		LeftObject.localPosition = new Vector3 (LeftObject.localPosition.x + MoveAmount, LeftObject.localPosition.y);
	}


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public float CameraDistance;
	private GameObject target;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (target == null) {
			target = GameObject.FindGameObjectWithTag ("Player");
		} else {
			transform.position = new Vector3 (target.transform.position.x, 0, CameraDistance);
		}
	}
}

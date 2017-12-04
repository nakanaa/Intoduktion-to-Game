using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSky : MonoBehaviour {

	void FixedUpdate () {
		if (Camera.main != null) {
			transform.position = new Vector3 (Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
		}
	}
}

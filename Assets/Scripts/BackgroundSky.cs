using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSky : MonoBehaviour {

	public Texture Nigth;
	public Texture Morning;
	public Texture Day;

	public float MorningX_Pos;
	public float DayX_Pos;

	void FixedUpdate () {
		if (Camera.main != null) {
			transform.position = new Vector3 (Camera.main.transform.position.x, Camera.main.transform.position.y, 1);
		}

		if (transform.position.x > MorningX_Pos && transform.position.x < DayX_Pos) {
			GetComponent<Renderer> ().material.mainTexture = Morning;
		} else if (transform.position.x > DayX_Pos) {
			GetComponent<Renderer> ().material.mainTexture = Day;
		} else {
			GetComponent<Renderer> ().material.mainTexture = Nigth;
		}

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkController : MonoBehaviour {

	public string PlayerPrefabName;

	// Use this for initialization
	void Start () {
		PhotonNetwork.ConnectUsingSettings ("0.1");

	}

	public void OnJoinedLobby(){
		PhotonNetwork.JoinRandomRoom ();
	}

	void OnPhotonRandomJoinFailed(){
		PhotonNetwork.CreateRoom (null);
	}

	void OnJoinedRoom(){
		print ("HERE");
		GameObject player = PhotonNetwork.Instantiate (PlayerPrefabName, Vector3.zero, Quaternion.identity,0);
	}
}

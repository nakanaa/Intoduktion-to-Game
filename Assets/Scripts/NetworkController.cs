using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkController : MonoBehaviour {

	public GameObject Player;
	public int SendRate;

	public static float NetworkLerp = 15;

	// Use this for initialization
	void Start () {
		PhotonNetwork.ConnectUsingSettings ("0.1");
		PhotonNetwork.sendRateOnSerialize = SendRate;
	}

	public void OnJoinedLobby(){
		PhotonNetwork.JoinRandomRoom ();
	}

	void OnPhotonRandomJoinFailed(){
		PhotonNetwork.CreateRoom (null);
	}

	void OnJoinedRoom(){
		GameObject player = PhotonNetwork.Instantiate (Player.name, Vector3.zero, Quaternion.identity,0);
	}
}

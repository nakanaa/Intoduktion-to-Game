using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkController : MonoBehaviour {

	public GameObject Spawnpoint;

	public GameObject Player;
	public int SendRate;

	public static float NetworkLerp = 15;

	private bool foundMasterClient;
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
		if (PhotonNetwork.player.IsMasterClient) {
			GameObject player = PhotonNetwork.Instantiate (Player.name, Vector3.zero, Quaternion.identity, 0);
			player.transform.position = Spawnpoint.transform.position;
			foundMasterClient = true;
		}
	}

	void Update()
	{
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		if (foundMasterClient == false) {
			SpawnToMasterClient ();
		}

	}

	private void SpawnToMasterClient()
	{
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject master in players) 
		{
			if (master.GetComponent<PhotonView> ().ownerId == PhotonNetwork.masterClient.ID) 
			{
				if (master.transform.position != Vector3.zero) 
				{
					GameObject player = PhotonNetwork.Instantiate (Player.name, Vector3.zero, Quaternion.identity, 0);
					player.transform.position = master.GetComponent<PlayerController>().networkPos;
					foundMasterClient = true;
				}
			}
		}
	}

}

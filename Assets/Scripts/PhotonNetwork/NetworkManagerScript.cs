using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerScript : MonoBehaviour {

    public Text TextInfos;
    //public Transform SpawnPoint_A;
    // Use this for initialization
    void Start () {
        PhotonNetwork.ConnectUsingSettings("v01");

	}
	
	// Update is called once per frame
	void Update () {

        if (PhotonNetwork.connectionStateDetailed.ToString() != "Joined")
        {
            TextInfos.text = PhotonNetwork.connectionStateDetailed.ToString();
        }
        else
        {
            TextInfos.text = "Connected to " + PhotonNetwork.room.name + "Player(s) Online " + PhotonNetwork.room.PlayerCount;
        }


	}

    void OnConnectedToMaster()
    {
        Debug.Log("Connected with Master");
        PhotonNetwork.JoinLobby();
    }

    void OnJoinedLobby()
    {
        RoomOptions MyRoomOptions = new RoomOptions();
        MyRoomOptions.MaxPlayers = 3;

        PhotonNetwork.JoinOrCreateRoom("Room1", MyRoomOptions, TypedLobby.Default);
        Debug.Log("Connected with Lobby"); 
    }

    void OnJoinedRoom()
    {
        if (PhotonNetwork.playerList.Length > 1)
        {
            StartCoroutine(SpawnMyPlayer());    
        }
        else
        {
            StartCoroutine(SpawnMyPlayer2());
        }
    }


    IEnumerator SpawnMyPlayer()
    {
        yield return new WaitForSeconds(1);
        //GameObject MyPlayer = PhotonNetwork.Instantiate("Tank", SpawnPoint_A.position, Quaternion.identity, 0) as GameObject;
    }
    IEnumerator SpawnMyPlayer2()
    {
        yield return new WaitForSeconds(1);
       // GameObject MyPlayer = PhotonNetwork.Instantiate("Tank", SpawnPoint_B.position, Quaternion.identity, 0) as GameObject;
    }
}
 
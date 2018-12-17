using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System;
using UnityEngine.UI;


public partial class NetworkManager : Photon.PunBehaviour {


    public static NetworkManager Singleton
    {
        get
        {
            return Instance;
        }
    }

    static NetworkManager Instance;
    public string _gameVersion = "0.1";

    //public GameObject _playerPrefab_1;
    //public GameObject _playerPrefab_2;

    public Button _startButton;
    public Button _leaveRoomButton;

    private void Awake()
    {
        Instance = this;
        PhotonNetwork.autoJoinLobby = false;
        PhotonNetwork.automaticallySyncScene = true;

        //button event listener
        _startButton.onClick.AddListener(Connect);
        _leaveRoomButton.onClick.AddListener(LeaveRoom);

    }

    void Connect()
    {
        if(PhotonNetwork.connected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings(_gameVersion);
        }
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    void LeaveLobby()
    {
        PhotonNetwork.LeaveLobby();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() 已被呼叫");
        PhotonNetwork.JoinRandomRoom();
    }


    public override void OnDisconnectedFromPhoton()
    {
        Debug.Log("OnDisconnectedFromPhoton() 已被呼叫");
    }

    /// <summary>
    /// 當PhotonNetwork.JoinRandomRoom()失敗時呼叫
    /// </summary>
    /// <param name="codeAndMsg">Code and message.</param>
    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        Debug.Log("OnPhotonRandomJoinFailed() 已被呼叫");
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 2 }, null);

    }

    /// <summary>
    /// 進入房間時呼叫
    /// </summary>
    public override void OnJoinedRoom()
    {
        GameObject player1;
        GameObject player2;

        //player1 = Instantiate(_playerPrefab_1, Vector3.zero, Quaternion.identity);
        //player2 = Instantiate(_playerPrefab_2, Vector3.zero, Quaternion.identity);

        //player1.SetActive(false);
        //player2.SetActive(false);

        if(PhotonNetwork.room.PlayerCount == 1)
        {
            //player1.SetActive(true);
            player1 = PhotonNetwork.Instantiate("Player (1)", Vector3.zero, Quaternion.identity,0);
        }
        else if (PhotonNetwork.room.PlayerCount == 2)
        {
            //player1.SetActive(true);
            //player2.SetActive(true);
            player2 = PhotonNetwork.Instantiate("Player (2)", new Vector3(-1,0,-2), Quaternion.identity, 0);
        }

        Debug.Log("OnJoinedRoom() 已被PUN呼叫，此玩家已進入房間");
    }

}

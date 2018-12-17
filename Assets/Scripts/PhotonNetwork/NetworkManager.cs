using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System;
using UnityEngine.UI;


public partial class NetworkManager : Photon.PunBehaviour {

    static NetworkManager Instance;

    public string _gameVersion = "0.1";

    [Header("是否一進開啟遊戲自動連線")]
    public bool isAutoConnect = true;


    [Space(10)]
    [Header("玩家在Resource資料夾的名稱")]
    public string PlayerName_1 = "Player_1_Ethan";
    public string PlayerName_2 = "Player_2_Ethan";

    [Header("設定按鈕")]
    [Tooltip("設定開始按鈕，若沒有設置則自動尋找")]
    public Button _startButton;
    [Tooltip("設定開始按鈕，若沒有設置則自動尋找")]
    public Button _leaveRoomButton;

    private Vector3 _playerFirstPosition_1;
    [Header("設置玩家起始位置")]
    [Tooltip("設定玩家一初始位置")]
    public Transform First_player_position;
    [HideInInspector]
    public Vector3 playerFirstPosition_1
    {
        get { 
            _playerFirstPosition_1 = First_player_position.position;
            return _playerFirstPosition_1; 
        }
        set { _playerFirstPosition_1 = value; }
    }

    private Vector3 _playerFirstPosition_2;
    [Tooltip("設定玩家二初始位置")]
    public Transform Second_player_position;
    [HideInInspector]
    public Vector3 playerFirstPosition_2
    {
        get
        {
            _playerFirstPosition_2 = Second_player_position.position;
            return _playerFirstPosition_2;
        }
        set { _playerFirstPosition_2 = value; }
    }



    private void Awake()
    {
        Instance = this;
        PhotonNetwork.autoJoinLobby = false;
        PhotonNetwork.automaticallySyncScene = true;

        if(_startButton == null)
            _startButton = GameObject.Find("StartButton").GetComponent<Button>();

        if (_startButton == null)
            _leaveRoomButton = GameObject.Find("DisconnectButton").GetComponent<Button>();

        //button event listener
        _startButton.onClick.AddListener(Connect);
        _leaveRoomButton.onClick.AddListener(LeaveRoom);

    }
    private void Start()
    {
        if (isAutoConnect)
            Connect();
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
        _startButton.gameObject.SetActive(true);
        _leaveRoomButton.gameObject.SetActive(false);
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

        if(PhotonNetwork.room.PlayerCount == 1)
        {
            player1 = PhotonNetwork.Instantiate(PlayerName_1, playerFirstPosition_1, Quaternion.identity,0);
            player1.transform.GetChild(0).tag = "Player";
            player1.transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (PhotonNetwork.room.PlayerCount == 2)
        {
            player2 = PhotonNetwork.Instantiate(PlayerName_2, playerFirstPosition_2, Quaternion.identity, 0);
            player2.transform.GetChild(0).tag = "Player";
            player2.transform.GetChild(1).gameObject.SetActive(true);
        }
        _startButton.gameObject.SetActive(false);
        _leaveRoomButton.gameObject.SetActive(true);

        Debug.Log("OnJoinedRoom() 已被PUN呼叫，此玩家已進入房間");
    }

}

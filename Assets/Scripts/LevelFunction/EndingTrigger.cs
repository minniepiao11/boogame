using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingTrigger : MonoBehaviour {
    GameManager manager;
    int playerNumber;
	// Use this for initialization
	void Start () {
        manager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        if (manager == null) Debug.LogError("There isnt have a GameObject with GameManager Tag");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player") return;

        switch (manager.stage)
        {
            case GameStage.playing:
                manager.stage = GameStage.onePlayerArriveAtTheEnd;
                manager.OnePlayerArriveEndEvent();
                return;

            case GameStage.onePlayerArriveAtTheEnd:
                manager.stage = GameStage.allPlayerArriveAtTheEnd;
                Debug.Log("End");
                manager.EndingEvent();
                return;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Player") return;

        switch(manager.stage)
        {
            case GameStage.onePlayerArriveAtTheEnd:
                manager.stage = GameStage.playing;
                return;
            case GameStage.allPlayerArriveAtTheEnd:
                manager.stage = GameStage.onePlayerArriveAtTheEnd;
                return;
        }

    }
}

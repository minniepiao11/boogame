using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameStage
{
    start = 1,
    playing,
    onePlayerArriveAtTheEnd,
    allPlayerArriveAtTheEnd
}
public class GameManager : MonoBehaviour {
    public GameStage stage = GameStage.start;
    private int _score;
    public int score
    {
        get { return _score; }
    }

    private float _clearStageTime;
    public float clearStageTime
    {
        get { return _clearStageTime; }
    }
    private GameObject[] Energy;
    void Initiate()
    {
        //check GameStage
        stage = GameStage.start;

        Energy = GameObject.FindGameObjectsWithTag("Energy");
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

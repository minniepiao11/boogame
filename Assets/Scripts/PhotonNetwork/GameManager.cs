using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum GameStage
{
    start = 1,
    playing,
    onePlayerArriveAtTheEnd,
    allPlayerArriveAtTheEnd
}
public class GameManager : MonoBehaviour {
    public Text ScoreText;
    public GameStage stage = GameStage.start;
    private int _score;
    public int score
    {
        get { return _score; }
        set { _score = value; }
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
    private void Update()
    {
        ScoreText.text = score.ToString();
    }
}

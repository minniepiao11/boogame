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
    public Text InforText;
    public GameStage stage = GameStage.start;
    private int _score = 0;
    public int playerNumber;
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
    //private GameObject[] Energy;
    void Initiate()
    {
        //check GameStage
        stage = GameStage.start;

        //Energy = GameObject.FindGameObjectsWithTag("Energy");
    }
    public void OnePlayerArriveEndEvent()
    {
        InforText.text = "Waiting for another player...";
    }
    public void EndingEvent()
    {
        string result = "";
        InforText.text = "score : " + score + "/n" + "Time : " + Mathf.Floor(Time.time) + "s";
        Debug.Log(InforText.text);

    }
    private void Start()
    {
        Initiate();
        stage = GameStage.playing;
    }
    private void FixedUpdate()
    {
        ScoreText.text = score.ToString();

        if (stage == GameStage.onePlayerArriveAtTheEnd || playerNumber ==1)
        {
            OnePlayerArriveEndEvent();
        }
        else if (stage == GameStage.allPlayerArriveAtTheEnd || playerNumber == 2)
        {
            EndingEvent();
        }
    }

}

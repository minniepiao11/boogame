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
    public void OnePlayerArriveEndEvent()
    {
        InforText.text = "Waiting for another player...";
    }
    public void EndingEvent()
    {
        string result = "";
        //switch(score)
        //{
        //    case 0:
        //        result = "score0";//"你是不知道有能量這個東西？"
        //        return;
        //    case 1:
        //        result = "score1";//"加油好嗎？"
        //        return;
        //    case 2:
        //        result = "score2";//"好棒棒喔，差一個就蒐集完了"
        //        return;
        //    case 3:
        //        result = "score3";//"我...我才不會誇獎你呢 哼"
        //        return;
        //}
        InforText.text = "score : " + score + "/n" + "Time : " + Mathf.Floor(Time.time) + "s";
        Debug.Log(InforText.text);

    }
    private void Start()
    {
        Initiate();
        stage = GameStage.playing;
    }
    private void Update()
    {
        ScoreText.text = score.ToString();
    }
}

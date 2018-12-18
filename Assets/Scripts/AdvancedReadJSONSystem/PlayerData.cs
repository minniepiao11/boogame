using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WeatherStation;

public class PlayerData : MonoBehaviour {
    
    SaveAndLoad SL;
    public Text playerName;
    public Text InputData;
	// Use this for initialization
    public void saveButton()
    {
        playerDataType p = new playerDataType();
        string _playerName = InputData.text;
        p.name = _playerName;
        SL.SaveData(p);
    }
    public void LoadButton()
    {
        playerDataType p = (playerDataType)SL.LoadData(typeof(playerDataType));
        playerName.text = "Current Name:" + p.name;
        Debug.Log(p);
    }
	void Start () {
        SL = GetComponent<SaveAndLoad>();
        LoadButton();

	}
	
}
public class playerDataType
{
    public string name = "player";
}

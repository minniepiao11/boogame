using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StartGameScript : MonoBehaviour {
    public Button StartGameButton;
    public GameObject NetworkManagerObject;
	// Use this for initialization
	void Start () {
        StartGameButton.onClick.AddListener(startGameFunc);
	}
	
	// Update is called once per frame
	void startGameFunc() {
        StartGameButton.gameObject.SetActive(false);
        NetworkManagerObject.SetActive(true);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Testing_info : MonoBehaviour {
    public GameObject player;
    public Text playerY;
    public GameObject target;
    public Text TargetY;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        playerY.text = "player yPos" + player.transform.position;
        TargetY.text = "Target yPos" + target.transform.position;
	}
}

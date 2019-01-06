using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour {
    GameManager m_GameManager;

	// Use this for initialization
	void Start () {
        
        m_GameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        if (m_GameManager == null) Debug.LogError("There isnt have a GameObject with GameManager Tag");

	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player") return;

    }
}

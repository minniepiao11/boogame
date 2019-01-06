using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisible : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (!(other.gameObject.tag == "Player"))
            return;
        this.GetComponent<MeshRenderer>().enabled = false;
        transform.parent.GetComponent<AudioSource>().Play();
    }
    private void OnTriggerExit(Collider other)
    {
        if (!(other.gameObject.tag == "Player"))
            return;
        this.GetComponent<MeshRenderer>().enabled = true;
    }
}

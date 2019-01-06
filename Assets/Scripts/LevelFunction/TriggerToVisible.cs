using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerToVisible : MonoBehaviour {
    public GameObject[] VisibleTarget;
	// Use this for initialization
	void Start () {
        foreach(GameObject _visibleObject in VisibleTarget)
            _visibleObject.GetComponent<MeshRenderer>().enabled = false;
	}
    private void OnTriggerEnter(Collider other)
    {
        if (!(other.gameObject.tag == "Player"))
            return;
        foreach (GameObject _visibleObject in VisibleTarget)
            _visibleObject.GetComponent<MeshRenderer>().enabled = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (!(other.gameObject.tag == "Player"))
            return;
        foreach (GameObject _visibleObject in VisibleTarget)
            _visibleObject.GetComponent<MeshRenderer>().enabled = false;
    }
}

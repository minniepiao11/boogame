using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAround : MonoBehaviour {
    
    public Transform center;
    Vector3 centerPosition;
    public float speed = 1;
	// Use this for initialization
	void Start () {
        centerPosition = center.position;
	}

    private void FixedUpdate()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
        transform.LookAt(center);
        //transform.Rotate(centerPosition, Mathf.Sin(Time.deltaTime) * speed);
    }
}

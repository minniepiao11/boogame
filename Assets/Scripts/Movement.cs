using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Movement : MonoBehaviour {
    Transform m_trans;
    Rigidbody m_rig;
    public float speed = 1f; 
	// Use this for initialization
	void Start () {
        m_trans = GetComponent<Transform>();
        m_rig = GetComponent<Rigidbody>();
	}
    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 _movement = new Vector3(h * Time.deltaTime * speed, 0, v * Time.deltaTime * speed);
        m_rig.MovePosition(this.transform.position + _movement);

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            m_rig.velocity += new Vector3(0, 5, 0);
        }
    }

    public void moveRight()
    {
        m_trans.position += m_trans.right.normalized * speed;
    }

    public void moveLeft()
    {
        m_trans.position += m_trans.right.normalized * speed * -1;
    }

    public void moveFront()
    {
        m_trans.position += m_trans.forward.normalized * speed;
    }

    public void moveBack()
    {
        m_trans.position += m_trans.forward.normalized * speed * -1;
    }

}

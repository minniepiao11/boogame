using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingFunction : MonoBehaviour {

    public float jumpVelocity_y = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player") return;

        Rigidbody _rigidbobdy = other.GetComponent<Rigidbody>();
        _rigidbobdy.velocity = new Vector3(_rigidbobdy.velocity.x, jumpVelocity_y, _rigidbobdy.velocity.z);    
        transform.parent.GetComponent<AudioSource>().Play();
    }
}

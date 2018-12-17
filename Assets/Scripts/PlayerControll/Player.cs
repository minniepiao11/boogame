using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Permissions;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour {

    public float gravity = -20;
    private Vector3 velocity = Vector3.zero;
    private PlayerController controller;


    private void Start()
    {
        controller = GetComponent<PlayerController>();
    }
    private void Update()
    {
        velocity.y += gravity * Time.deltaTime;
        //controller.Move(ref velocity * Time.deltaTime); 
    }
}

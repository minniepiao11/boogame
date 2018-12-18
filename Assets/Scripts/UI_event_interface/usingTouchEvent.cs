using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchEvent_handler;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class usingTouchEvent : MonoBehaviour {

    public Text text;

    CharacterController controller;
    Transform groundChecker;
    public float GroundDistance;
    public LayerMask groundMask;

    public float DashDistance = 0.5f;
    //Rigidbody playerRig;
    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;

    Vector3 moveVelocity;
    float gravity;
    float jumpVelocity;
    public Vector3 Drag = new Vector3(8, 0, 8);
    public float MoveSpeed = 1f;

    MobileTouchEventDetetor mobileTouch = new MobileTouchEventDetetor();

	// Use this for initialization
	void Start () {
        if(text == null)
            text = GameObject.Find("EventText").GetComponent<Text>();
        //playerRig = Player.GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        groundChecker = transform.GetChild(0);


        jumpVelocity = (jumpHeight * 2) / timeToJumpApex;
        gravity = -jumpVelocity / timeToJumpApex;

	}

	
    private void FixedUpdate()
    {
        
        mobileTouch.TouchEventDetect();

        Vector2 _input = mobileTouch.joystick.virtualJousticInput;

        moveVelocity.x = _input.x * MoveSpeed * Time.deltaTime;
        moveVelocity.z = _input.y * MoveSpeed * Time.deltaTime;

        moveVelocity.y += gravity * Mathf.Pow(Time.deltaTime, 2);
       
        bool isQuickTap = mobileTouch.quickTapEvent.isQuickTap;
        bool isGrounded = Physics.CheckSphere(
            groundChecker.position,
            GroundDistance,
            groundMask,
            QueryTriggerInteraction.Ignore);

        text.text = "input : " + "("+ _input.x + "," + _input.y + ")" + 
                    " isQuickTap: " + isQuickTap + 
                    " isGrounded: " + isGrounded +
                    " Gravity : " + gravity +
                    " moveVelocity : " + moveVelocity;
        //轉正方向
        //if (moveVelocity != Vector3.zero)
            //transform.forward = new Vector3(moveVelocity.x, 0, moveVelocity.z);
        
       

        //跳躍
        if (mobileTouch.quickTapEvent.isQuickTap && isGrounded)
        {
            text.text = "Quick Double Tap";
            moveVelocity.y += jumpVelocity * Time.deltaTime;
        }

        //衝刺
        //if(mobileTouch.swipeEvent.isSwiping)
        //{
        //    text.text = "Dash";
        //    moveVelocity += Vector3.Scale((MobileInputData.endPos - MobileInputData.startPos).normalized,DashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * Drag.x + 1)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * Drag.z + 1)) / -Time.deltaTime)));
        //}

        //判斷是否在地上
        if (isGrounded && moveVelocity.y < 0)
            moveVelocity.y = 0;

       

        moveVelocity.x /= 1 + Drag.x * Time.deltaTime;
        moveVelocity.z /= 1 + Drag.z * Time.deltaTime;
        moveVelocity.y /= 1 + Drag.y * Time.deltaTime;

        controller.Move(moveVelocity);

        
    }
}

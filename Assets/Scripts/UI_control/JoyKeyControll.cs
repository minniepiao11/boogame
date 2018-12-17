using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 請將此腳本掛在玩家身上
/// </summary>
public class JoyKeyControll : MonoBehaviour {
    /// <summary>
    /// The bottom joycon trans.
    /// 最底層JoyCon必須要有父物件
    /// </summary>
    public RectTransform bottom_Joycon_trans;
    public RectTransform Top_Joycon_trans;
    public Transform Player_trans;

    Vector3 Movement;
    Vector3 _mousePosition;
    public float speed = 0.002f;
    Touch touch;

    private void Awake()
    {
        Player_trans = GetComponent<Transform>();
        bottom_Joycon_trans.gameObject.transform.parent.gameObject.active = false;
    }
    private void Start()
    {
        touch = Input.GetTouch(0);

    }
    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            //Vuforia.CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
            _mousePosition = Input.mousePosition;
            bottom_Joycon_trans.gameObject.transform.parent.gameObject.active = true;
            bottom_Joycon_trans.gameObject.transform.parent.position = Input.mousePosition;
        }
        if (Input.GetButton("Fire1"))
        {
            if (Vector3.Distance(Top_Joycon_trans.localPosition + Input.mousePosition - _mousePosition, Vector3.zero) < 225)
            {
                Movement = Top_Joycon_trans.localPosition - bottom_Joycon_trans.localPosition;
                Top_Joycon_trans.localPosition += Input.mousePosition - _mousePosition;
            }

            Player_trans.gameObject.GetComponent<Rigidbody>().MovePosition(Player_trans.position + new Vector3(Movement.x,0,Movement.y)*Time.deltaTime*speed);
        }
        if (Input.GetButtonUp("Fire1"))
        {
            Top_Joycon_trans.localPosition = bottom_Joycon_trans.localPosition;
            bottom_Joycon_trans.gameObject.transform.parent.gameObject.active = false;
        }
        _mousePosition = Input.mousePosition;


	}
    //detect 
    public bool DoubleTab()
    {
        bool isHappening = true;
        return isHappening;
    }
    public bool quickSwipe()
    {
        bool isHappening = true;
        return isHappening;
    }

    public void resetPosition()
    {
        Player_trans.position = Vector3.zero;
    } 
}

/*
if(Input.touchCount == 1)
{
    var t1 = Input.GetTouch(0);
    //Vector2 firstTouch;//first toush point
    if (t1.phase == TouchPhase.Began)
    {
        bottom_Joycon_trans.gameObject.transform.parent.gameObject.active = true;
    }
    if (t1.phase == TouchPhase.Moved)
    {
        float distance = Vector3.Distance(Top_Joycon_trans.localPosition, Vector3.zero);
        if(distance<225)//225是半徑
            Top_Joycon_trans.localPosition += (Vector3)t1.deltaPosition;
        Movement = Top_Joycon_trans.localPosition - Vector3.zero;
        Player_trans.gameObject.GetComponent<Rigidbody>().MovePosition(Movement);
    }
    if (t1.phase == TouchPhase.Ended)
    {
        bottom_Joycon_trans.gameObject.transform.parent.gameObject.active = false;
        Top_Joycon_trans.localPosition = bottom_Joycon_trans.localPosition;
    }
}
*/

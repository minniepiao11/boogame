// <summary>
// 感謝Unity應用領域的大大們：Parsue Choi、林士揚、Cheng-Yuan Cheng
// 原文網址：https://m.facebook.com/groups/581769871867384?view=permalink&id=2134350483275974
// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Security.Cryptography.X509Certificates;
using System;
using System.Diagnostics;

namespace TouchEvent_handler
{
    
    public struct MobileInputData
    {
        public static float vertical = 0;
        public static float horizontal = 0;
        public static float intervals;//按下去間隔時間
        public static float lastTouchTime;

        public static Vector2 startPos ;//觸碰起始點
        public static Vector2 endPos ;//觸碰結束點
        public static Vector2 direction ;//移動方向

        //changeable setting data
        public static float joystickRadius = 150f;
        public static float maxＨoldingTime = 3;//按住多久才會達到滿的狀態
        public static float maxSwipeInterval = 0.2f;
        public static float quickDoubleTabInterval = 0.15f;
    }

    /// <summary>
    /// 負責規範更新MobileInputData的衍生類別
    /// </summary>
    public abstract class TouchEventMain : MonoBehaviour{
        public QuickDoubleTapEvent quickTapEvent;
        public QuickSwipeEvent swipeEvent;
        public PureHoldingEvent holdEvent;
        public JoystickEvent joystick;

        protected float begainTime = 0f;//最初點擊時間
        protected Touch lastTouch;//目前沒用到，這個是用來記錄上一次的觸碰
        protected float lastTouchTime;  //上一次點擊放開的時間

        /// <summary>
        /// the time record the touch time from began to now
        /// </summary>
        protected float touchInterval;
        public abstract void TouchEventDetect();
    }

    /// <summary>
    /// 負責更新MobileInputData裡面的資料
    /// </summary>
    public class MobileTouchEventDetetor : TouchEventMain
    {
        public MobileTouchEventDetetor ()
        {
            quickTapEvent = new QuickDoubleTapEvent();
            swipeEvent = new QuickSwipeEvent();
            holdEvent = new PureHoldingEvent();
            joystick = new JoystickEvent();
        }
        private void Update()
        {
            //TouchEventDetect();
        }
        public override void TouchEventDetect()
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                bool isTouchUIElement = EventSystem.current.IsPointerOverGameObject(touch.fingerId);

                if (!isTouchUIElement)
                {
                    switch (touch.phase)
                    {
                        case TouchPhase.Began://點下去的狀態
                            MobileInputData.startPos = touch.position;
                            begainTime = Time.realtimeSinceStartup;
                            break;


                        case TouchPhase.Moved://手按住滑動的狀態
                            MobileInputData.direction = touch.position - MobileInputData.startPos;
                            MobileInputData.intervals = Time.realtimeSinceStartup - begainTime;
                            break;

                        case TouchPhase.Stationary://手按住不動的狀態
                            MobileInputData.intervals = Time.realtimeSinceStartup - begainTime;
                            break;

                        case TouchPhase.Ended://手離開螢幕時的狀態
                            MobileInputData.intervals = Time.realtimeSinceStartup - begainTime;
                            MobileInputData.lastTouchTime = Time.realtimeSinceStartup;
                            MobileInputData.endPos = MobileInputData.startPos + MobileInputData.direction;
                            lastTouch = touch;
                            break;

                        case TouchPhase.Canceled:
                            MobileInputData.intervals = Time.realtimeSinceStartup - begainTime;
                            MobileInputData.lastTouchTime = Time.realtimeSinceStartup;
                            MobileInputData.endPos = MobileInputData.startPos + MobileInputData.direction;
                            lastTouch = touch;
                            break;

                    }
                }
            }
        }
    }

    /// <summary>
    /// 連擊抽象類別
    /// </summary>
    public abstract class QuickTapEvent{
        protected float _lastTouchTime;
        public bool isQuickTap;
        public abstract bool CheckIfQuickTabEventHappening();
    }

    /// <summary>
    /// 滑動抽象類別
    /// </summary>
    public abstract class SwipeEvent 
    {
        protected float _minSwipeDistance;
        protected float _maxSwipeTimeInterval;
        protected float _touchInterval;
        public  bool _isSwiping;
        public abstract bool CheckIfSwipeEventHappening();
    }

    /// <summary>
    /// 按住抽象類別
    /// </summary>
    public abstract class HoldingEvent
    {
        public abstract bool CheckIfHoldingEventHappening();
    }

    /// <summary>
    /// Joystic 抽象類別
    /// </summary>
    public abstract class Joystick 
    {
        public abstract Vector2 InputWithJoystick(Vector2 startPos, Vector2 _Input, float joystickRadius);
    }

    /// <summary>
    /// Quick double tap event.
    /// </summary>
    public class QuickDoubleTapEvent : QuickTapEvent
    {
        
        public bool isQuickTap{
            get{
                if (Input.touchCount == 1)
                {
                    Touch touch = Input.GetTouch(0);
                    bool isTouchUIElement = EventSystem.current.IsPointerOverGameObject(touch.fingerId);

                    if (!isTouchUIElement )
                    {
                        if (touch.phase == TouchPhase.Began)
                        { 
                            return CheckIfQuickTabEventHappening();
                        }
                    }
                }
                return false;
            }
        }

        public override bool CheckIfQuickTabEventHappening()
        {
            if (Time.realtimeSinceStartup - MobileInputData.lastTouchTime < MobileInputData.quickDoubleTabInterval)
            {
                return true;
            }
            return false;
        }
    }

    public class JoystickEvent : Joystick
    {
        public Vector2 virtualJousticInput{
            get{
                if (Input.touchCount == 1)
                {
                    Touch touch = Input.GetTouch(0);
                    bool isTouchUIElement = EventSystem.current.IsPointerOverGameObject(touch.fingerId);

                    if (!isTouchUIElement & MobileInputData.intervals > 0.2f)
                    {
                        return InputWithJoystick(
                            MobileInputData.startPos,
                            Input.GetTouch(0).position,
                            MobileInputData.joystickRadius);
                    }
                }
                return Vector2.zero;
            }
        }

        /// <summary>
        /// return a vector2 value between -1 to 1
        /// </summary>
        /// <returns>The with joystick.</returns>
        /// <param name="startPos">Start Touch position.</param>
        /// <param name="_Input">InputTouch.</param>
        /// <param name="joystickRadius">Joystick radius.</param>
        public override Vector2 InputWithJoystick(Vector2 startPos, Vector2 _Input, float joystickRadius)
        {
            Vector2 input = Vector2.zero;

            input = (_Input - startPos).magnitude >= joystickRadius ? 
                                       (_Input - startPos).normalized * joystickRadius :
                                       (_Input - startPos);
            
            input.x = input.x / joystickRadius;
            input.y = input.y / joystickRadius;

            MobileInputData.horizontal = input.x;
            MobileInputData.vertical = input.y;

            return input;
        }
    }

    public class PureHoldingEvent : HoldingEvent
    {
        public float holdingPercent = 0;

        /// <summary>
        /// check if is holding(only read)
        /// </summary>
        /// <value><c>true</c> if is holding; otherwise, <c>false</c>.</value>
        public bool isHolding{ get { return CheckIfHoldingEventHappening(); }}

        /// <summary>
        /// 還沒寫完
        /// </summary>
        /// <returns><c>true</c>, if if holding event happening was checked, <c>false</c> otherwise.</returns>
        public override bool CheckIfHoldingEventHappening()
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                bool isTouchUIElement = EventSystem.current.IsPointerOverGameObject(touch.fingerId);

                if (!isTouchUIElement)
                {
                    if (touch.phase == TouchPhase.Moved||
                        touch.phase == TouchPhase.Stationary)
                    {
                        if (MobileInputData.intervals > 0.3f)
                        {
                            holdingPercent = (MobileInputData.intervals / MobileInputData.maxＨoldingTime) * 100;

                            if (MobileInputData.intervals > MobileInputData.maxＨoldingTime)
                            {
                                holdingPercent = 100;
                            }

                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }

    public class QuickSwipeEvent : SwipeEvent
    {
        public bool isSwiping {get { return CheckIfSwipeEventHappening(); }}
        public override bool CheckIfSwipeEventHappening()
        {
            _minSwipeDistance = 120;
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                bool isTouchUIElement = EventSystem.current.IsPointerOverGameObject(touch.fingerId);

                if (!isTouchUIElement)
                {
                    if (touch.phase == TouchPhase.Ended ||
                        touch.phase == TouchPhase.Canceled)
                    {
                        if (MobileInputData.intervals < MobileInputData.maxSwipeInterval &
                            MobileInputData.direction.magnitude > _minSwipeDistance)
                            MobileInputData.intervals = 0;
                            return true;
                    }
                }
            }
            return false;
        }
    }

}
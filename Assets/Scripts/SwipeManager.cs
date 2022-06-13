using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//делегаты - это такие типы, которые представляют собой ссылки на другие методы, которые имеют ту же сигнатуру
public class SwipeManager : MonoBehaviour
{
    public static SwipeManager instance;
    public enum Direction { Left, Right, Up, Down };

    bool[] swipe = new bool[4];
    bool touchMoved;

    Vector2 _startTouch;
    Vector2 _swipeDelta;

    const float SWIPE_THRFSHOLD = 40;

    public delegate void MoveDelegate(bool[] swipes);
    public MoveDelegate MoveEvent;

    public delegate void ClickDelegate(Vector2 pos);
    public ClickDelegate ClickEvent;
    Vector2 TouchPosition()
    {
        return (Vector2)Input.mousePosition;
    }
    bool TouchBegan()
    {
        return Input.GetMouseButtonDown(0);
    }
    bool TouchEnded()
    {
        return Input.GetMouseButtonUp(0);
    }
    bool GetTouch()
    {
        return Input.GetMouseButton(0);
    }
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    { //start finish
        if (EventSystem.current.IsPointerOverGameObject()) return; //проверка чтобы не нажимало клик на кнопки.
        if (TouchBegan())
        {
            _startTouch = TouchPosition();
            touchMoved = true;
        }
        else if (TouchEnded() && touchMoved == true)
        {
            SendSwipe();
            touchMoved = false;
        }
        //calculate swipe
        _swipeDelta = Vector2.zero;
        if (touchMoved && GetTouch())
        {
            _swipeDelta = TouchPosition() - _startTouch;
        }
        //check swipe
        if (_swipeDelta.magnitude > SWIPE_THRFSHOLD)// чтобы узнать длину можно иисспользовать magnitude
        {
            if (Mathf.Abs(_swipeDelta.x) > Mathf.Abs(_swipeDelta.y))
            {
                //left/right
                swipe[(int)Direction.Left] = _swipeDelta.x < 0;
                swipe[(int)Direction.Right] = _swipeDelta.x > 0;
            }
            else
            {
                //up/down
                swipe[(int)Direction.Down] = _swipeDelta.y < 0;
                swipe[(int)Direction.Up] = _swipeDelta.y > 0;
            }
            SendSwipe();
        }
    }
    void SendSwipe()
    {
        if (swipe[0] || swipe[1] || swipe[2] || swipe[3])
        {
            Debug.Log(swipe[0] + "|" + swipe[1] + "|" + swipe[2] + "|" + swipe[3]);
            MoveEvent?.Invoke(swipe); // if (MoveEvent != null)
            //{ MoveEvent(swipe); }
        }
        else
        {
            Debug.Log("Click");
            ClickEvent?.Invoke(TouchPosition());
        }
        Reset();
    }
    private void Reset()
    {
        _startTouch = _swipeDelta = Vector2.zero;
        touchMoved = false;
        for (int i = 0; i < 4; i++)
        {
            swipe[i] = false;
        }
    }
}
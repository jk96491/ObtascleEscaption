using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIJoystickController : MonoBehaviour {

    [SerializeField]
    private UISprite BG = null;
    [SerializeField]
    private UISprite ControlSprite = null;
    [SerializeField]
    private UIEventListener ControlEventListener = null;
    [SerializeField]
    private Transform ControlTrans = null;
    [SerializeField]
    private Vector2 firstVec = Vector2.zero;

    private void Start()
    {
        ControlEventListener.onDrag = OnDrag;
        ControlEventListener.onDragEnd = onDragEnd;
        ControlEventListener.onDragOver = onDragOver;
        ControlEventListener.onDragOut = onDragOut;
    }

    private Vector2 movePos = Vector2.zero;
    public Vector2 MovePos { get { return movePos; } }

    private void Update()
    {
        ControlTrans.localPosition = firstVec + movePos;
    }

    private void OnDrag(GameObject go, Vector2 position)
    {
        movePos = position * 3;
        //ControlTrans.localPosition = firstVec + position;

        Debug.LogError(string.Format("vec {0}", position));
    }

    private void onDragEnd(GameObject go)
    {
        // ControlTrans.localPosition = firstVec;
        movePos = Vector2.zero;
    }

    private void onDragOver(GameObject go)
    {
        //Debug.LogError("DragOver");
    }

    private void onDragOut(GameObject go)
    {
        //Debug.LogError("onDragOut");
    }
}

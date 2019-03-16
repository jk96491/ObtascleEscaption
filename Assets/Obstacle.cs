using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public bool active = false;
    public int delayCount = 0;
    public int usedPosIndex = 0;

    [SerializeField]
    private Transform Trans = null;
    [SerializeField]
    private GameObject Obj = null;

    public void SetPosition(Vector3 Pos)
    {
        if(Trans != null)
        {
            Trans.localPosition = Pos;
        }
    }

    public void MovePosition(Vector3 Pos)
    {
        if (Trans != null)
        {
            Trans.localPosition += Pos;
        }
    }

    public void SetActive(bool active_)
    {
        if(Obj != null)
        {
            Obj.SetActive(active_);
            active = active_;
        }
    }

    public Vector3 GetPosition()
    {
        return Trans.localPosition;
    }

}

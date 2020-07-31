using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxEffect : MonoBehaviour
{
    public float factor;
    public Transform target;
    public bool doResetPos;

    public bool forceY;
    public float forceYOffset;

    private Vector3 originalPos;
    private Vector3 originalPos2;

    public void ResetPos(Vector3 newPos)
    {
        originalPos = newPos;
        originalPos2 = target.position;
    }
    public void CalcPos()
    {
        if(target!= null)
        {
            transform.position = (target.position - originalPos2) * factor + originalPos;
        }
        if (forceY)
        {
            Vector3 tmp = transform.position;
            tmp.y = target.position.y + forceYOffset;
            transform.position = tmp;
        }
    }
    private void FixedUpdate()
    {
        CalcPos();
    }
    private void Start()
    {
        ResetPos(transform.position);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public ZhabaController zhabka;
    public bool isHead;
    public LayerMask ground;

    private void Update()
    {
        if (GetComponent<BoxCollider2D>().IsTouchingLayers(ground))
        {
            zhabka.Death();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isHead)
        {

        }
        else
        {
            if (collision.GetComponent<ZhabaController>() != null)
            {
                collision.GetComponent<ZhabaController>().Death();
            }
        }
    }
}

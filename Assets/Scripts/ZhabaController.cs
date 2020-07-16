using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZhabaController : MonoBehaviour
{
    private Rigidbody2D RB;
    public float minSpeed;
    public float maxSpeed;
    public float speedUp = 0.1f;
    public GameObject cameraPoint;
    public Vector3 cameraPointPos;
    
    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        RB.velocity = new Vector2(minSpeed, 0f);
    }

    private void Update()
    {
        cameraPoint.transform.position = transform.position + cameraPointPos;
    }
    void FixedUpdate()
    {
        /*
        if (RB.velocity.x < minSpeed) RB.velocity = RB.velocity.normalized * minSpeed;
        if (RB.velocity.x > maxSpeed) RB.velocity = RB.velocity.normalized * maxSpeed;
     
        RB.velocity = new Vector2(RB.velocity.x + speedUp * Time.fixedDeltaTime, RB.velocity.y);*/

        RB.velocity = new Vector2(minSpeed, RB.velocity.y);
    }
}

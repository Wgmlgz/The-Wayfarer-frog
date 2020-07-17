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

    public bool clipMode = false;
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
        if (clipMode)
        {
            RB.bodyType = RigidbodyType2D.Kinematic;
            GetComponent<CapsuleCollider2D>().enabled = false;

            RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, 999), -Vector2.up);
            Vector2 tmp = (new Vector2(transform.position.x + minSpeed * Time.fixedDeltaTime, hit.point.y));

            RB.position = tmp;

            transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(hit.normal.y, hit.normal.x) * Mathf.Rad2Deg - 90f, Vector3.forward);

        }
        else
        {
            RB.bodyType = RigidbodyType2D.Dynamic;
            GetComponent<CapsuleCollider2D>().enabled = true;

            if (RB.velocity.magnitude < minSpeed) RB.velocity = RB.velocity.normalized * minSpeed;
            if (RB.velocity.magnitude > maxSpeed) RB.velocity = RB.velocity.normalized * maxSpeed;

            RB.velocity = new Vector2(RB.velocity.x + speedUp * Time.fixedDeltaTime, RB.velocity.y);

            var angle = Mathf.Atan2(RB.velocity.y, RB.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        //RB.velocity = new Vector2(minSpeed, RB.velocity.y);
    }
}

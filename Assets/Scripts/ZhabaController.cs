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

    public float angleSmooth = 1f;
    public float posSmooth = 1f;

    public GameObject body;

    private float jumpTime = 0f;
    public float maxjumpTime = 1f;
    public float jumpHieght = 10;

    private Vector2 tmpVelosity;
    private Vector3 lastFixedUpdateFramePos;
    private Vector3 lastUpdateFramePos;

    public GameObject indicator;

    public bool clipMode = false;
    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        RB.velocity = new Vector2(minSpeed, 0f);
    }
    private void FixedUpdate()
    {
        jumpTime += Time.fixedDeltaTime;
        if (!clipMode)
        {
            RB.bodyType = RigidbodyType2D.Dynamic;
            GetComponent<CapsuleCollider2D>().enabled = true;

            if (RB.velocity.magnitude < minSpeed) RB.velocity = RB.velocity.normalized * minSpeed;
            if (RB.velocity.magnitude > maxSpeed) RB.velocity = RB.velocity.normalized * maxSpeed;

            RB.velocity = new Vector2(RB.velocity.x + speedUp * Time.fixedDeltaTime, RB.velocity.y);

            //RB.velocity = RB.velocity.normalized * minSpeed;

            Quaternion tq = Quaternion.AngleAxis(Mathf.Atan2(RB.velocity.y, RB.velocity.x) * Mathf.Rad2Deg, Vector3.forward);
            if (GetComponent<CapsuleCollider2D>().IsTouchingLayers())
            {

                if (jumpTime > maxjumpTime) {
                    clipMode = true;
                }
            }
            else
            {}
        }
        else
        {
            Debug.Log(1f / Time.fixedDeltaTime);
            tmpVelosity = (1f / Time.fixedDeltaTime) * (transform.position - lastFixedUpdateFramePos);
            lastFixedUpdateFramePos = transform.position;
        }

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            clipMode = false;
            RB.bodyType = RigidbodyType2D.Dynamic;
            RB.velocity = tmpVelosity;
            RB.velocity = new Vector2(RB.velocity.x, jumpHieght);
            jumpTime = 0f;
        }
        if (clipMode)
        {
            RB.velocity = Vector2.zero;
            RB.bodyType = RigidbodyType2D.Kinematic;
            GetComponent<CapsuleCollider2D>().enabled = false;
            /*
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, 999), -Vector2.up);
            Vector2 tmp = (new Vector2(transform.position.x + minSpeed * Time.deltaTime, hit.point.y));
            
            indicator.transform.position = transform.rotation * Vector3.up * 5 + transform.rotation.normalized * Vector2.right * minSpeed * Time.deltaTime;
            //indicator.transform.rotation = (transform.rotation * -Vector3.up);
            */

            RaycastHit2D hit = Physics2D.Raycast(
                transform.rotation * Vector3.up * 999 + transform.rotation.normalized * Vector2.right * minSpeed * Time.deltaTime + transform.position,
                (transform.rotation * -Vector3.up)
            );

            transform.position = hit.point;

            //transform.rotation;
            Quaternion tq = Quaternion.AngleAxis(Mathf.Atan2(hit.normal.y, hit.normal.x) * Mathf.Rad2Deg - 90f, Vector3.forward);

            transform.rotation = Quaternion.Lerp(transform.rotation, tq, Time.deltaTime * angleSmooth);
        }

        body.transform.position = Vector3.Lerp(body.transform.position, transform.position, posSmooth * Time.deltaTime);
        body.transform.rotation = transform.rotation;

        lastUpdateFramePos = transform.position;
        //RB.veloci+ty = new Vector2(minSpeed, RB.velocity.y);
    }
}

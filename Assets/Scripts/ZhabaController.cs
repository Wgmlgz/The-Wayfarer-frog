using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZhabaController : MonoBehaviour
{
    [Header("global")]
        public bool isPlaying = false;
        public bool clipMode = false;

    [Header("phisics control")]
        public float minSpeed;
        public float maxSpeed;
        public float speedUp = 0.1f;

        private float jumpTime = 0f;
        public float maxjumpTime = 1f;
        public float jumpHieght = 10;

        public float rotationSpeed = 1f;

    [Header("smooth")]
        public float angleSmooth = 1f;
        public float posSmooth = 1f;

    [Header("graphics")]
        public GameObject body;
        public ParticleSystem sandParticles;

    [Header("cam control")]
        public Cinemachine.CinemachineVirtualCamera sceneCam;
        public Cinemachine.CinemachineVirtualCamera gameCam;

    //private
        private Rigidbody2D RB;
        private Vector2 tmpVelosity;
        private Vector3 lastFixedUpdateFramePos;
        private Vector3 lastUpdateFramePos;

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        //RB.velocity = new Vector2(minSpeed, 0f);
    }
    private void FixedUpdate()
    {
        if (isPlaying)
        {
            jumpTime += Time.fixedDeltaTime;
            if (!clipMode)
            {
                sandParticles.Pause();
                RB.bodyType = RigidbodyType2D.Dynamic;
                GetComponent<CapsuleCollider2D>().enabled = true;

                //if (RB.velocity.magnitude < minSpeed) RB.velocity = RB.velocity.normalized * minSpeed;
                //if (RB.velocity.magnitude > maxSpeed) RB.velocity = RB.velocity.normalized * maxSpeed;

                //RB.velocity = new Vector2(RB.velocity.x + speedUp * Time.fixedDeltaTime, RB.velocity.y);

                //RB.velocity = RB.velocity.normalized * minSpeed;

                Quaternion tq = Quaternion.AngleAxis(Mathf.Atan2(RB.velocity.y, RB.velocity.x) * Mathf.Rad2Deg, Vector3.forward);
                if (GetComponent<CapsuleCollider2D>().IsTouchingLayers())
                {
                    transform.rotation = Quaternion.identity;
                    if (jumpTime > maxjumpTime)
                    {
                        clipMode = true;
                    }
                }
                else if (Input.GetKey(KeyCode.Space))
                {
                    transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + rotationSpeed * Time.fixedDeltaTime);
                }
            }
            else
            {
                tmpVelosity = (1f / Time.fixedDeltaTime) * (transform.position - lastFixedUpdateFramePos);
                lastFixedUpdateFramePos = transform.position;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.Space))
            {
                ToGame();
                //Jump();
            }
        }
    }
    void Update()
    {
        if (isPlaying)
        {
            if (clipMode)
            {
                sandParticles.Play();
                RB.velocity = Vector2.zero;
                RB.bodyType = RigidbodyType2D.Kinematic;

                //raycast
                RaycastHit2D[] hitAll = Physics2D.RaycastAll(
                    transform.rotation * Vector3.up * 5 + transform.rotation.normalized * Vector2.right * minSpeed * Time.deltaTime + transform.position,
                    (transform.rotation * -Vector3.up)
                );
                RaycastHit2D hit = new RaycastHit2D();
                foreach (RaycastHit2D i in hitAll)
                {
                    if (i.transform.gameObject.CompareTag("Ground"))
                    {
                        hit = i;
                        break;
                    }
                }
                transform.position = hit.point;

                //calc rotation
                Quaternion tq = Quaternion.AngleAxis(Mathf.Atan2(hit.normal.y, hit.normal.x) * Mathf.Rad2Deg - 90f, Vector3.forward);
                transform.rotation = Quaternion.Lerp(transform.rotation, tq, Time.deltaTime * angleSmooth);

                //if jump
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();
                }
            }

            lastUpdateFramePos = transform.position;

            /*RaycastHit2D[] hitAllCam = Physics2D.RaycastAll(
                   Vector3.up * 5 + transform.position,
                   (-Vector3.up)
               );
            RaycastHit2D hitCam = new RaycastHit2D();
            foreach (RaycastHit2D i in hitAllCam)
            {
                if (i.transform.gameObject.CompareTag("Ground"))
                {
                    hitCam = i;
                    break;
                }
            }
            cameraPoint.transform.position = Vector3.Lerp(cameraPoint.transform.position, hitCam.point, camSmooth * Time.deltaTime);*/
        }

        //smooth pos
        body.transform.position = Vector3.Lerp(body.transform.position, transform.position, posSmooth * Time.deltaTime);
        body.transform.rotation = transform.rotation;
    }
    void ToGame()
    {
        isPlaying = true;
        sceneCam.gameObject.SetActive(false);
        gameCam.gameObject.SetActive(true);

        RB.velocity = new Vector2(minSpeed, 0f);
    }
    void Death()
    {

    }
    void Jump()
    {
        clipMode = false;
        RB.bodyType = RigidbodyType2D.Dynamic;
        tmpVelosity.y = jumpHieght;
        RB.velocity = tmpVelosity;
        //RB.velocity = (RB.velocity + (Vector2)(Vector3.up * jumpHieght));
        jumpTime = 0f;
    }
}

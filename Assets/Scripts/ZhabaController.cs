using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZhabaController : MonoBehaviour
{
    [Header("global")]
        public bool isPlaying = false;
        public bool clipMode = false;
        public UnityEvent onPlay;
        public UnityEvent onDeath;

    [Header("phisics control")]
        public float minSpeed;
        public float curSpeed;
        public float maxSpeed;
        public float speedUp = 0.1f;

        private float jumpTime = 0f;
        public float maxjumpTime = 1f;
        public float jumpHieght = 10;

        public float rotationSpeed = 1f;
        public float rotationSpeedAuto = 1f;

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
        private bool inputA;

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
                    if (transform.rotation.eulerAngles.z > 180)
                    {
                        Death();
                    }
                    
                    if (jumpTime > maxjumpTime)
                    {
                        body.transform.rotation = Quaternion.identity;
                        curSpeed = RB.velocity.magnitude;
                        clipMode = true;
                    }
                }
                else if (Input.GetKey(KeyCode.Space) || inputA)
                {
                    body.transform.rotation = Quaternion.Euler(0, 0, body.transform.rotation.eulerAngles.z + rotationSpeed * Time.fixedDeltaTime);
                }
                else
                {
                    body.transform.rotation =
                        Quaternion.Lerp(body.transform.rotation,
                        Quaternion.AngleAxis(Mathf.Atan2(RB.velocity.y, RB.velocity.x) * Mathf.Rad2Deg, Vector3.forward),
                        Time.deltaTime * rotationSpeedAuto);
                }
            }
            else
            {
                lastFixedUpdateFramePos = transform.position;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.Space) || inputA)
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

                //calc velosity
                curSpeed += (-tmpVelosity.y - 5) * speedUp * Time.deltaTime;
                if (curSpeed < minSpeed) curSpeed = minSpeed;
                if (curSpeed > maxSpeed) curSpeed = maxSpeed;

                //raycast
                RaycastHit2D[] hitAll = Physics2D.RaycastAll(
                    transform.rotation * Vector3.up * 5 + transform.rotation.normalized * Vector2.right * curSpeed * Time.deltaTime + transform.position,
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
                if (Input.GetKeyDown(KeyCode.Space) || inputA)
                {
                    Jump();
                }
            }

            tmpVelosity = (1f / Time.deltaTime) * (transform.position - lastUpdateFramePos);
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
        body.transform.position = Vector3.Lerp(body.transform.position, transform.position, posSmooth * Time.deltaTime);
    }
    private void LateUpdate()
    {
        if (clipMode)
        {
            body.transform.rotation = transform.rotation;
        }
    }
    public void ToGame()
    {
        isPlaying = true;
        sceneCam.gameObject.SetActive(false);
        gameCam.gameObject.SetActive(true);

        RB.velocity = new Vector2(minSpeed, 0f);

        onPlay.Invoke();
    }
    void Death()
    {
        return;
        isPlaying = false;
        onDeath.Invoke();
    }
    public void SetTimeScale(float i)
    {
        Time.timeScale = i;
    }
    public void Jump(float hieght = -1f)
    {
        body.transform.rotation = transform.rotation;
        clipMode = false;
        RB.bodyType = RigidbodyType2D.Dynamic;

        if (hieght == -1f) tmpVelosity.y = jumpHieght;
        else if (hieght == 0f) tmpVelosity.y = tmpVelosity.y;
        else tmpVelosity.y = hieght;

        RB.velocity = tmpVelosity;
        //RB.velocity = (RB.velocity + (Vector2)(Vector3.up * jumpHieght));
        jumpTime = 0f;
    }
    public void InputAOn()
    {
        inputA = true;
    }
    public void InputAOff()
    {
        inputA = false;
    }
}

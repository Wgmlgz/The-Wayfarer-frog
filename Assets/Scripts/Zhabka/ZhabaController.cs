using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class ZhabaController : MonoBehaviour
{
    [Header("global")]
        public bool isGod;
        public bool isPlaying = false;
        public bool stopPlay = false;
        public bool clipMode = false;
        public LayerMask ground;
        public UnityEvent onPlay;
        public UnityEvent onDeath;

    [Header("gameplay control")]
        public float deathAngle;
        public float flipTmp;
        public float lastFFlipTmp;
        public float scoreAddFactor;
        public int tmpScore;
        public bool dubJ;

    [Header("Abilities")]
        public bool canDoubleJump;
        public bool canInfJump;
        public bool canFall;
        public bool fly;
        public int cactusesLeft;
        public bool ignoreHead;
        //public float coinMod = 1f;
        public float rotMod = 1f;
        public bool canSecondLife;
        private bool secendLifeUsed;

    [Header("physics control")]
        public float minSpeed;
        public float curSpeed;
        public float maxSpeed;
        public float startMaxSpeed;
        public float maxXSpeed;
        public float maxXSpeedDist = 1000;
        public float speedUp = 0.1f;

        private float jumpTime = 0f;
        public float maxjumpTime = 1f;
        public float jumpHieght = 10;

        public float rotationSpeed = 1f;
        public float rotationSpeedAuto = 1f;

    [Header("smooth")]
        public float angleSmooth = 1f;
        public float posSmooth = 1f;
        public float camSmooth = 1f;

    [Header("graphics")]
        public GameObject body;
        //public GameObject head;
        public ParticleSystem sandParticles;

    [Header("cam control")]
        public Cinemachine.CinemachineVirtualCamera sceneCam;
        public Cinemachine.CinemachineVirtualCamera gameCam;
        [Range(0f, 100f)] public float camSize = 50f;
        [Range(0f, 1f)] public float camMult = .5f;
        public float offsetFactor = 1.5f;

    //private
        private Rigidbody2D RB;
        public Vector2 tmpVelosity;
        public Vector3 lastUpdateFramePos;
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

    }
    void Update()
    {
        if (isPlaying)
        {
            jumpTime += Time.deltaTime;
            RB.simulated = true;
            
            if (clipMode)
            {
                flipTmp = 0;
                lastFFlipTmp = 0;

                GetComponent<Score>().cleanScoreTime = 0;
                sandParticles.Play();
                RB.velocity = Vector2.zero;
                RB.bodyType = RigidbodyType2D.Kinematic;

                dubJ = false;

                //calc velosity
                curSpeed += (-tmpVelosity.y - 8) * speedUp * Time.deltaTime;
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
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();
                }
                tmpVelosity = (1f / Time.deltaTime) * (transform.position - lastUpdateFramePos);
            }
            else
            {
                GetComponent<Score>().cleanScoreTime = 9999;

                sandParticles.Pause();
                RB.bodyType = RigidbodyType2D.Dynamic;
                GetComponent<CapsuleCollider2D>().enabled = true;

                //if (RB.velocity.magnitude < minSpeed) RB.velocity = RB.velocity.normalized * minSpeed;
                //if (RB.velocity.magnitude > maxSpeed) RB.velocity = RB.velocity.normalized * maxSpeed;

                //RB.velocity = new Vector2(RB.velocity.x + speedUp * Time.fixedDeltaTime, RB.velocity.y);

                //RB.velocity = RB.velocity.normalized * minSpeed;

                Quaternion tq = Quaternion.AngleAxis(Mathf.Atan2(RB.velocity.y, RB.velocity.x) * Mathf.Rad2Deg, Vector3.forward);
                if (GetComponent<CapsuleCollider2D>().IsTouchingLayers(ground))
                {
                    //death check
                    RaycastHit2D[] hitAll = Physics2D.RaycastAll(transform.rotation * Vector3.up * 5, (transform.rotation * -Vector3.up));
                    RaycastHit2D hit = new RaycastHit2D();
                    foreach (RaycastHit2D i in hitAll)
                    {
                        if (i.transform.gameObject.CompareTag("Ground"))
                        {
                            hit = i;
                            break;
                        }
                    }
                    float tmpAngle = Mathf.Abs(
                        Quaternion.Angle(
                        body.transform.rotation,
                        Quaternion.AngleAxis(Mathf.Atan2(hit.normal.x, hit.normal.y) * Mathf.Rad2Deg, Vector3.forward))
                    );


                    if (jumpTime > maxjumpTime)
                    {
                        float t = body.transform.rotation.eulerAngles.z;
                        if (t > 180) t = 360 - t;
                        if (t > deathAngle) Death("head");

                        body.transform.rotation = Quaternion.identity;
                        curSpeed += (GetComponent<Score>().score - tmpScore) * scoreAddFactor;
                        clipMode = true;
                    }
                }
                else if (Input.GetKey(KeyCode.Space) || inputA)
                {
                    body.transform.rotation = Quaternion.Euler(0, 0, body.transform.rotation.eulerAngles.z + rotationSpeed * Time.deltaTime * rotMod);
                    flipTmp += (rotationSpeed * Time.deltaTime * rotMod);
                    if (flipTmp > 300 && lastFFlipTmp < 300)
                    {
                        GetComponent<Score>().AddScore(5, "flip");
                        //flipTmp = 0;
                    }
                    else if (flipTmp > 630 && lastFFlipTmp < 630)
                    {
                        GetComponent<Score>().AddScore(15, "double flip");
                    }
                    else if (flipTmp > 960 && lastFFlipTmp < 960)
                    {
                        GetComponent<Score>().AddScore(30, "flip flip flip");
                    }
                    else if (flipTmp > 1260 && lastFFlipTmp < 1260)
                    {
                        GetComponent<Score>().AddScore(50, "flip flip flip flip");
                    }
                    else if (flipTmp > 1560 && lastFFlipTmp < 1560)
                    {
                        GetComponent<Score>().AddScore(100, "OMG is it real????????? (flip flip flip flip flip)");
                    }
                    else if (flipTmp > 1860 && lastFFlipTmp < 1860)
                    {
                        GetComponent<Score>().AddScore(200, "U r > ilskr flip flip flip flip flip flip)");
                    }
                    else if (flipTmp > 2160 && lastFFlipTmp < 2160)
                    {
                        GetComponent<Score>().AddScore(500, "f f f f f f f l l l l l l l i i i i i i i p p p p p p p");
                    }
                    else if (flipTmp > 2460 && lastFFlipTmp < 2460)
                    {
                        GetComponent<Score>().AddScore(1000, "ffffffff8llllllll8iiiiiiii8pppppppp8");
                    }
                    lastFFlipTmp = flipTmp;
                }
                else
                {
                    body.transform.rotation =
                        Quaternion.Lerp(body.transform.rotation,
                        Quaternion.AngleAxis(Mathf.Atan2(RB.velocity.y, RB.velocity.x) * Mathf.Rad2Deg, Vector3.forward),
                        Time.deltaTime * rotationSpeedAuto);
                }
            }

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
        else
        {
            if (Input.GetKey(KeyCode.Space) || inputA)
            {
                ToGame();
                //Jump();
            }
            sandParticles.Pause();
            RB.simulated = false;
        }

        body.transform.position = Vector3.Lerp(body.transform.position, transform.position, posSmooth * Time.deltaTime);
        lastUpdateFramePos = transform.position;
    }
    private void LateUpdate()
    {
        if (clipMode)
        {
            body.transform.rotation = transform.rotation;
        }
        gameCam.m_Lens.OrthographicSize = Mathf.Lerp(gameCam.m_Lens.OrthographicSize, ((curSpeed + camSize) * camMult), camSmooth * Time.deltaTime);
        Vector3 tmp = gameCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
        tmp.x = gameCam.m_Lens.OrthographicSize * offsetFactor;
        gameCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = tmp;

        maxSpeed = (GetComponent<Score>().distance / maxXSpeedDist) * (maxXSpeed - startMaxSpeed) + startMaxSpeed;
        if((GetComponent<Score>().distance / maxXSpeedDist) > 1f) maxSpeed = maxXSpeed;
        //head.transform.rotation = body.transform.rotation;
    }
    public void SetToGod()
    {
        isGod = true;
    }
    public void SetToPlaying()
    {
        isPlaying = true;
    }
    public void ToGame()
    {
        if (stopPlay) return;
        isPlaying = true;
        sceneCam.gameObject.SetActive(false);
        gameCam.gameObject.SetActive(true);

        RB.velocity = new Vector2(minSpeed, 0f);

        onPlay.Invoke();
    }
    public void Death(string s = "ded")
    {
        if (isGod) return;
        if(s == "cactus")
        {
            cactusesLeft--;
            if (cactusesLeft >= 0) return;
        }
        if(s == "head")
        {
            if (ignoreHead) return;
        }
        isPlaying = false;
        stopPlay = true;
        
        onDeath.Invoke();
    }
    public void SetTimeScale(float i)
    {
        Time.timeScale = i;
    }
    public void Jump(float hieght = -1f)
    {
        if (canDoubleJump)
        {
            if (clipMode)
            {

            }
            else
            {
                if (dubJ) return;
                else dubJ = true;
            }
        }
        else if (canInfJump)
        {
            if (clipMode)
            {

            }
            else
            {

            }
        }
        else
        {
            if (!clipMode) return;
            else
            {
                body.transform.rotation = transform.rotation;
            }
        }



        if (hieght == -1f)
        {
            tmpScore = GetComponent<Score>().score;
            GetComponent<Score>().AddScore(1, "jump");
            tmpVelosity.y = jumpHieght;
        }
        else if (hieght == 0f)
        {
            if (!clipMode) return;
        }
        else tmpVelosity.y = hieght;

        clipMode = false;
        RB.bodyType = RigidbodyType2D.Dynamic;
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
    public void SetIsPlay(bool b)
    {
        isPlaying = b;
    }
    public void Fall()
    {
        if (!canFall) return;
        transform.rotation = Quaternion.identity;
        clipMode = true;
    }

    public void Slow(){
        curSpeed = minSpeed;
    }
    public void Fast(){
        curSpeed = maxSpeed;
    }
}

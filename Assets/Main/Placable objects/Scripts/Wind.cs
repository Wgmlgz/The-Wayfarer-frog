using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour {
    Vector2 direction;
    float force;
    public bool active = false;
    [Range(0, 1000f)] public float velocity;
    private GameObject ZHB;
    private GameObject ZH;
    private ZhabaController ZHC;
    public ParticleSystem PS;
    public Vector2 customRotDir;


    private float timeLastTouch;

    private void Awake() {
        ZHB = GameObject.FindGameObjectWithTag("ZhabkaBody");
        ZH = GameObject.FindGameObjectWithTag("Player");
        PS = GameObject.FindGameObjectWithTag("WindParticles").GetComponent<ParticleSystem>();
        ZHC = ZH.GetComponent<ZhabaController>();
    }

    public void ToActive() {
        active = true;
        ZH.GetComponent<ZhabaController>().doCustomRotDir = true;
        ZH.GetComponent<ZhabaController>().customRotDir = customRotDir;
        ZH.GetComponent<Rigidbody2D>().gravityScale = 2f;
        GameObject.FindGameObjectWithTag("tutorial").GetComponent<Tutorial>().tryShow();
    }

    public void EndEffect() {
        active = false;
        ZH.GetComponent<ZhabaController>().doCustomRotDir = false;
        ZH.GetComponent<Rigidbody2D>().gravityScale = 6f;

        var emission = PS.emission;
        emission.rateOverTime = 0;
        //ZH.GetComponent<ZhabaController>().customRotDir = customRotDir;
    }
    private void Update() {
        if (active) {
            ApplyForce();
        } else {

        }
    }
    public void ApplyForce() {
        timeLastTouch -= Time.deltaTime;
        // calc angle factor
        float angle = ZHB.transform.rotation.eulerAngles.z;
        if (angle < 90f) {
            angle /= 45f;
            if (angle > 1) angle = 1 - (angle - 1);
            //angle *= 1.3f;
            //angle -= .3f;
        } else {
            angle = 0;
        }

        // calc dist factor
        var zhp = ZH.transform.position;

        var emission = PS.emission;
        if (angle > 0 && ZHC.jumpTime > ZHC.maxjumpTime) {
            ZH.GetComponent<Rigidbody2D>().velocity =
            new Vector2(
                Mathf.Lerp(ZH.GetComponent<Rigidbody2D>().velocity.x,
                    ZH.GetComponent<ZhabaController>().minSpeed,
                    Time.deltaTime * 0.5f),
                angle * velocity
                //* distFactor
                );
            emission.rateOverTime = 80f * angle + 20;
        } else {
            emission.rateOverTime = 0;
        }
    }
    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject == ZH) {
            ToActive();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject == ZH) {
            EndEffect();
        }
    }
}

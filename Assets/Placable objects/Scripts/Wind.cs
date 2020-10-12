using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    Vector2 direction;
    float force;
    public bool active;
    [Range(0, 1000f)]public float velocity;
    private GameObject ZHB;
    private GameObject ZH;
    public ParticleSystem PS;
    public Vector2 customRotDir;
    
    private void Awake() {
        ZHB = GameObject.FindGameObjectWithTag("ZhabkaBody");
        ZH = GameObject.FindGameObjectWithTag("Player");
    }

    public void ToActive (){
        active = true;
        ZH.GetComponent<ZhabaController>().doCustomRotDir = true;
        ZH.GetComponent<ZhabaController>().customRotDir = customRotDir;
        ZH.GetComponent<Rigidbody2D>().gravityScale = 2f;
    }

    public void EndEffect() {
        active = false;
        ZH.GetComponent<ZhabaController>().doCustomRotDir = false;
        ZH.GetComponent<Rigidbody2D>().gravityScale = 6f;
        //ZH.GetComponent<ZhabaController>().customRotDir = customRotDir;
    }
    private void Update() {
        if (active) {
            ApplyForce();
        } else {
            var emission = PS.emission;
            emission.rateOverTime = 0;
        }
    }
    public void ApplyForce(){
        float angle = ZHB.transform.rotation.eulerAngles.z;

        if (angle < 90f) {
            angle /= 45f;
            if (angle > 1) angle = 1 - (angle - 1);
            angle *= 1.3f;
            angle -= 0.3f;
        } else {
            angle = 0;
        }

        var emission = PS.emission;
        if (angle > 0) {
            ZH.GetComponent<Rigidbody2D>().velocity =
            new Vector2(ZH.GetComponent<Rigidbody2D>().velocity.x, angle * velocity);
            emission.rateOverTime = 80f * angle + 20;
        } else {
            emission.rateOverTime = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
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

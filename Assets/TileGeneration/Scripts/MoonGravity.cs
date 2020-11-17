using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonGravity : MonoBehaviour {
    public float gravity_scale = 1f;
    public bool active = false;
    private GameObject ZH;

    private float timeLastTouch;

    private void Awake() {
        ZH = GameObject.FindGameObjectWithTag("Player");
    }
    public void ToActive() {
        active = true;
        ZH.GetComponent<Rigidbody2D>().gravityScale = gravity_scale;
    }
    public void EndEffect() {
        active = false;
        ZH.GetComponent<Rigidbody2D>().gravityScale = 6f;
    }
    private void Update() {
        if (active) {
            ApplyForce();
        } else {

        }
    }
    public void ApplyForce() {
        
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

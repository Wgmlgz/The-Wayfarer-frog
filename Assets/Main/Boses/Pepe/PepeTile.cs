using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PepeTile : MonoBehaviour {
    public bool active = false;
    private GameObject ZHB;
    private GameObject ZH;
    private ZhabaController ZHC;

    private void Awake() {
        ZHB = GameObject.FindGameObjectWithTag("ZhabkaBody");
        ZH = GameObject.FindGameObjectWithTag("Player");
        ZHC = ZH.GetComponent<ZhabaController>();

    }
    private void Update() {
        if (active) {
            float angle = ZHB.transform.rotation.eulerAngles.z;
        } else {

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
    public void ToActive() {
        active = true;
    }

    public void EndEffect() {
        active = false;
    }
}

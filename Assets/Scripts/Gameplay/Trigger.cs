using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour {
    public GameObject target;
    public UnityEvent onEnter;
    public UnityEvent onStay;
    public bool doOnce;
    private bool isOff;
    private void Awake() {
        if (target == null) target = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject == target) {
            if (isOff == false) {
                onEnter.Invoke();
                if (doOnce) {
                    isOff = true;
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject == target) {
            onStay.Invoke();
        }
    }
}

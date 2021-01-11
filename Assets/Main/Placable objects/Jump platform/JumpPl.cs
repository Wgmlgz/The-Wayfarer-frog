using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPl : MonoBehaviour {
    public GameObject zh;
    public Vector2 force;
    private void Awake() {
        zh = GameObject.FindGameObjectWithTag("Player");
    }
    public void Push() {
        Debug.Log(zh);
        zh.GetComponent<Rigidbody2D>().velocity =
            new Vector2(force.x + zh.GetComponent<Rigidbody2D>().velocity.x, force.y);
    }
}

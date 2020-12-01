using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstRotation : MonoBehaviour {


    void SetRotation() {
        transform.rotation = Quaternion.identity;
    }
    void Update() {
        SetRotation();
    }
    void LateUpdate() {
        SetRotation();
    }
    void FixedUpdate() {
        SetRotation();
    }
}

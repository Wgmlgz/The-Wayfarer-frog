using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZhCamTarget : MonoBehaviour {
    public bool da;
    private GameObject ZHB;

    private void Awake() {
        ZHB = GameObject.FindGameObjectWithTag("ZhabkaBody");
    }

    private void Update() {
      
    }
    private void LateUpdate() {
        if (!da) transform.position = ZHB.transform.position;
        //da = false;
    }
}

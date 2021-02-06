using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZhCamTarget : MonoBehaviour {
    public bool da;
    private ZhabaController ZH;
    private GameObject ZHB;
    public Vector3 lt;
    [HideInInspector] public float timer;
    private void Awake() {
        ZHB = GameObject.FindGameObjectWithTag("ZhabkaBody");
        ZH = GameObject.FindGameObjectWithTag("Player").GetComponent<ZhabaController>();
    }

    private void Update() {

    }
    private void LateUpdate() {
        lt = transform.position;
        if (!da) {
            transform.position = ZHB.transform.position;
            ZH.const_cam_size = -1;
        }
        timer += Time.deltaTime;
        if (timer > 0.5) da = false;
    }
}

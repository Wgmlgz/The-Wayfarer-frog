using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PepeRocket : MonoBehaviour {
    public GameObject rocket;
    private GameObject ZHB;
    public float target_rot;

    public float rot_speed = 90;
    private void Awake() {
        ZHB = GameObject.FindGameObjectWithTag("ZhabkaBody");
    }
    void SetNewRot() {
        target_rot = Random.Range(-180, 180);
    }

    private void Update() {
        float angle = transform.rotation.eulerAngles.z;
        angle -= target_rot;
        if (angle > 180) angle = -360 + angle;

        float da = angle < 0 ? -1 : 1;
        if (rot_speed * Time.deltaTime < da*angle) {
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + -da * rot_speed * Time.deltaTime);
        } else {
            transform.rotation = Quaternion.Euler(0, 0, target_rot);
            SetNewRot();
        }
        
        
    }



}

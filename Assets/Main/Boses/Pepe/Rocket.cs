using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rocket : MonoBehaviour {
    public GameObject rocket;
    public float rocket_speed;
    public Slider da;
    private GameObject ZH;
    private GameObject ZHB;
    public float live_offset = 70;

    public void LaunchRocket() {
        rocket.transform.localPosition = new Vector3(0f, -150f, 0f);
    }
    private void Awake() {
        ZHB = GameObject.FindGameObjectWithTag("ZhabkaBody");
        ZH = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update() {
        Debug.Log(Quaternion.Angle(ZHB.transform.rotation, rocket.transform.rotation));
        da.value = (Quaternion.Angle(ZHB.transform.rotation, rocket.transform.rotation) / 180);
        if (rocket.transform.localPosition.y < -2) {
            rocket.transform.localPosition =    
                new Vector3(0f, rocket.transform.localPosition.y + rocket_speed * Time.deltaTime, 0f);
        } else {
            if (Quaternion.Angle(ZHB.transform.rotation, rocket.transform.rotation) > live_offset) {
                ZH.GetComponent<ZhabaController>().Death();
            } else {

            }
            LaunchRocket();
        }
        
    }
}

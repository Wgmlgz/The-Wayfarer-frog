using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pepe : MonoBehaviour {
    public GameObject cam_target;
    public GameObject rotate_rocket;
    public bool fight;
    public float fight_timer;

    public bool is_in_wind;
    public void StartFight() {
        rotate_rocket.SetActive(true);
    }
    public void EndFight() {
        rotate_rocket.SetActive(false);
    }
    public void LeaveFight() {
        cam_target.GetComponent<ZhCamTarget>().da = false;
    }
    private void Update() {
        if (fight) {
            fight_timer += Time.deltaTime;
        }
    }
    private void LateUpdate() {
        if (is_in_wind == false) {
            LeaveFight();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {
    private ZhabaController ZH;

    private void Awake() {
        ZH = GameObject.FindGameObjectWithTag("Player").GetComponent<ZhabaController>();
        GetComponent<RandomActivator>().chance = ZH.heartSpawnChance;
        GetComponent<RandomActivator>().Refresh();
    }
}

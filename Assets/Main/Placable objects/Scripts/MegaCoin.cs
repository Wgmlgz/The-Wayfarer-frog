using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaCoin : MonoBehaviour {
    private ZhabaController ZH;

    private void Awake() {
        ZH = GameObject.FindGameObjectWithTag("Player").GetComponent<ZhabaController>();
        GetComponent<RandomActivator>().chance = ZH.megaCoinSpawnChance;
        GetComponent<RandomActivator>().Refresh();
    }
}

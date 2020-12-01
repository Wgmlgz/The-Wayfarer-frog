using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInfo : MonoBehaviour {
    public string NPCName;

    public GameObject zhabka;

    private void Start() {
        bool canLive = zhabka.GetComponent<NPCManager>().LifeRequest(GetComponent<NPCInfo>());
        if (!canLive) {
            Destroy(gameObject);
        } else if (NPCName == "Box") {
            zhabka.GetComponent<NPCManager>().spawnedBox = GetComponent<Boss>();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInfo : MonoBehaviour
{
    public string NPCName;

    public GameObject zhabka;

    private void Start()
    {
        if (!zhabka.GetComponent<NPCManager>().LifeRequest(GetComponent<NPCInfo>()))
        {
            Destroy(gameObject);
        }
    }
}

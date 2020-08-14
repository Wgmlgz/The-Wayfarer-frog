using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPC
{
    public string NPCName = "Zhabka";
    public int spawnDistance = 50;
    public bool spawnOnce = true;
    public bool isSpawned = false;
}

public class NPCManager : MonoBehaviour
{
    public List<NPC> NPCs;
    public List<NPCInfo> whiteList;

    public bool LifeRequest(NPCInfo d)
    {
        foreach (var i in whiteList) if (i == d) return true;

        foreach (var i in NPCs)
        {
            if (
                (d.gameObject.transform.position - GameObject.FindGameObjectWithTag("Player").GetComponent<Score>().startPos).magnitude *
                GameObject.FindGameObjectWithTag("Player").GetComponent<Score>().distanceFactor < 
                i.spawnDistance) return false;

            if (i.NPCName == d.NPCName)
            {
                if (i.spawnOnce)
                {
                    if (i.isSpawned) return false;
                    else
                    {
                        i.isSpawned = true;
                        return true;
                    }
                }
                else return true;
            }
        }
        return false;
    }
}

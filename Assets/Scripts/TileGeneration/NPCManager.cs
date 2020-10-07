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

    public Box spawnedBox;
    public string place;
    public string oldPlace;

    public bool LifeRequest(NPCInfo d)
    {
        foreach (var i in whiteList) if (i == d) return true;

        foreach (var i in NPCs)
        {
            if (i.NPCName == d.NPCName)
            {
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<Score>().GetDist(d.gameObject.transform) < i.spawnDistance) {
                    Debug.Log(GameObject.FindGameObjectWithTag("Player").GetComponent<Score>().GetDist(d.gameObject.transform));
                    Debug.Log(i.spawnDistance);
                    return false;
                }
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

    public void StartBoxFight() {
        spawnedBox.StartFight();
    }
    public void SetPlace(string name) {
        place = name;
    }
    public void SetPlaceToBox() {
        SetPlace("Box");
    }
    private void Update() {
        
    }
    private void LateUpdate() {
        Debug.Log(oldPlace);
        Debug.Log(place);
        if (oldPlace == "Box" && place == "None") {
            BoxEnd();
        }
        if (oldPlace == "None" && place == "Box") {
            StartBoxFight();
        }

        oldPlace = place;
        place = "None";
    }
    void BoxEnd() {
        spawnedBox.EndFight();
    }

}

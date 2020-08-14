using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Arrows
{
    public GameObject ar1;
    public GameObject ar2;
}

public class ZhabaSelector : MonoBehaviour
{
    public int viewN;

    public int selectedN;

    public List<bool> isLock;
    public List<Cinemachine.CinemachineVirtualCamera> cams;
    public List<Arrows> ars;
    public List<TMPro.TextMeshProUGUI> staT;
    public List<GameObject> locks;
    public List<GameObject> toadSkins;

    private void Start()
    {
        selectedN = PlayerPrefs.GetInt("SelectedToad");
        SelectToad(selectedN);

        for (int i = 0; i < locks.Count; i++)
        {
            if(PlayerPrefs.GetInt("ToadLock" + i.ToString()) == 1)
            {
                LockToad(i);
            }
            else
            {
                UnlockToad(i);
            }
        }
    }
    public void ViewLeft()
    {
        if(viewN > 0)
        {
            viewN -= 1;
            ViewInt(viewN);
        }
    }
    public void ViewRight()
    {
        if (viewN < cams.Count - 1)
        {
            viewN += 1;
            ViewInt(viewN);
        }
    }
    public void ViewInt(int j)
    {
        foreach (var i in ars)
        {
            i.ar1.SetActive(false);
            i.ar2.SetActive(false);
        }
        ars[j].ar1.SetActive(true);
        ars[j].ar2.SetActive(true);

        foreach (var i in cams)
        {
            i.Priority = -9999;
        }
        cams[j].Priority = 9999;
    }
    public void SelectToad(int j)
    {
        selectedN = j;
        PlayerPrefs.SetInt("SelectedToad", j);
        GameObject.FindGameObjectWithTag("Player").GetComponent<ZhabaController>().zhabaTipe = selectedN;

        foreach (var i in staT)
        {
            i.SetText("(tap on toad to select)");
        }
        staT[j].SetText("selected");

        foreach (var i in toadSkins)
        {
            i.SetActive(false);
        }
        toadSkins[j].SetActive(true);
    }
    public void LockToad(int j)
    {
        locks[j].SetActive(true);
        PlayerPrefs.SetInt("ToadLock" + j.ToString(), 1);
    }
    public void UnlockToad(int j)
    {
        locks[j].SetActive(false);
        PlayerPrefs.SetInt("ToadLock" + j.ToString(), 0);
    }
}

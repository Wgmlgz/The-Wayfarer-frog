using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Arrows
{
    public GameObject ar1;
    public GameObject ar2;
}
[System.Serializable]
public class Zhabka
{
    public GameObject skin;
    public List<GameObject> hats;
}
public class ZhabaSelector : MonoBehaviour
{
    public GameObject mainCam;
    public int viewN;

    public int selectedN;
    public int selectedHat;

    public List<bool> isLock;
    public List<Cinemachine.CinemachineVirtualCamera> cams;
    public List<Arrows> ars;
    public List<TMPro.TextMeshProUGUI> staT;
    public List<GameObject> locks;
    public List<Zhabka> toads;
    public List<GameObject> hatDA;
    public GameObject hatNET;

    private ZhabaController zh;

    private void Start()
    {
        zh = GameObject.FindGameObjectWithTag("Player").GetComponent<ZhabaController>();
        SelectToad(PlayerPrefs.GetInt("SelectedToad"));
        SetHat(PlayerPrefs.GetInt("SelectedHat"));

        for (int i = 0; i < locks.Count; i++)
        {
            if(PlayerPrefs.GetInt("ToadLock" + i.ToString()) == 1)
            {
                LockToad(i);
            }
            else
            {
                if (isLock[i])
                {
                    LockToad(i);
                }
                else
                {
                    UnlockToad(i);
                }
            }
        }


        //wednesday frog
        LockToad(2);
        if(System.DateTime.Now.DayOfWeek == System.DayOfWeek.Wednesday)
        {
            UnlockToad(2);
        }
    }
    public void ViewTop(){
        viewN = 0;
        ViewInt(0);
        cams[0].Priority = -9999;
        mainCam.SetActive(true);        
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

        foreach (var i in staT)
        {
            i.SetText("(tap on toad to select)");
        }
        staT[j].SetText("selected");

        foreach (var i in toads)
        {
            i.skin.SetActive(false);
        }
        toads[j].skin.SetActive(true);

        zh.canDoubleJump = false;
        zh.ignoreHead = false;
        zh.hpLeft = 0;
        zh.rotMod = 1;
        zh.canFall = false;
        zh.gameObject.GetComponent<CoinCollector>().coinMult = 1;

        if (selectedN == 0)
        {
            //
        }
        else if (selectedN == 1)
        {
            zh.hpLeft = 3;
        }
        else if (selectedN == 2)
        {
            zh.gameObject.GetComponent<CoinCollector>().coinMult *= 3;
            zh.canDoubleJump = true;
        }
        else if (selectedN == 3)
        {
            zh.canDoubleJump = true;
        }
        else if (selectedN == 4)
        {
            zh.ignoreHead = true;
            zh.hpLeft = 3;
        }
        else if (selectedN == 5)
        {
            zh.canDoubleJump = true;
            zh.rotMod = 1.5f;
            zh.gameObject.GetComponent<CoinCollector>().coinMult *= 2;
        }
        else if (selectedN == 6)
        {
            zh.canDoubleJump = true;
            zh.ignoreHead = true;
            zh.rotMod *= 2;
            zh.canFall = true;
            zh.hpLeft = 3;
            zh.gameObject.GetComponent<CoinCollector>().coinMult *= 4;
        }

        SetHatBonus();
    }
    public void LockToad(int j)
    {
        locks[j].SetActive(true);
        PlayerPrefs.SetInt("ToadLock" + j.ToString(), 1);
        if(selectedN == j)
        {
            selectedN = 0;
        }
    }
    public void UnlockToad(int j)
    {
        locks[j].SetActive(false);
        PlayerPrefs.SetInt("ToadLock" + j.ToString(), 0);
    }
    public void SetHat(int j)
    {
        selectedHat = j;
        PlayerPrefs.SetInt("SelectedHat", j);

        
        
        SelectToad(selectedN);
    }
    void SetHatBonus()
    {
        int j = selectedHat;
        
        foreach (var i in toads[selectedN].hats)
        {
            i.SetActive(false);
        }
        if(j != -1) toads[selectedN].hats[j].SetActive(true);

        foreach (var i in hatDA)
        {
            i.SetActive(false);
        }

        if(j != -1) hatDA[j].SetActive(true);
        hatNET.SetActive(j == -1);

        if (selectedHat == 0)
        {
            zh.rotMod *= 2;
        }
        else if (selectedHat == 1)
        {

        }
        else if (selectedHat == 2)
        {
            zh.hpLeft += 3;
        }
        else if (selectedHat == 3)
        {
            zh.ignoreHead = true;
        }
        else if (selectedHat == 4)
        {
            zh.gameObject.GetComponent<CoinCollector>().coinMult *= 2;
        }
        else if (selectedHat == 5)
        {
            zh.canFall = true;
        }
    }
}

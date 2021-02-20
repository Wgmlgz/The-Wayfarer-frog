using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Arrows {
    public GameObject ar1;
    public GameObject ar2;
}
[System.Serializable]
public struct UnlockRequirements {
    public int coins;
    public int level;
}
public class ZhabaSelector : MonoBehaviour {
    public GameObject mainCam;
    public int viewN;

    public int selectedN;
    public int selectedHat;

    public List<bool> isLock;
    public ZhabaSoul zsoul;
    public List<GameObject> hatDA;
    public GameObject hatNET;
    public List<ToadUnlockHandler> toads;

    private ZhabaController zh;

    private bool fin_setup = false;
    private void Start() {
        zh = GameObject.FindGameObjectWithTag("Player").GetComponent<ZhabaController>();

        int sel_t = PlayerPrefs.GetInt("SelectedToad");
        if (PlayerPrefs.HasKey("SelectedHat") == false) PlayerPrefs.SetInt("SelectedHat", -1);
        int sel_h = PlayerPrefs.GetInt("SelectedHat");

        for (int i = 1; i < toads.Count; i++) {
            if (PlayerPrefs.GetInt("ToadLock" + i.ToString()) == 1 || PlayerPrefs.HasKey("ToadLock" + i.ToString()) == false) {
                LockToad(i);
            } else {
                if (isLock[i]) {
                    LockToad(i);
                } else {
                    UnlockToad(i);
                }
            }
        }
        UnlockToad(0);

        //wednesday frog
        LockToad(2);
        if (System.DateTime.Now.DayOfWeek == System.DayOfWeek.Wednesday) {
            UnlockToad(2);
        } else {
            if (sel_t == 2) {
                sel_t = 0;
            }
        }
        SelectToad(sel_t);
        SetHat(sel_h);
        fin_setup = true;
    }
    public void ViewTop() {
        viewN = 0;
        ViewInt(0);
        toads[0].cam.Priority = -9999;
        mainCam.SetActive(true);
    }
    public void ViewLeft() {
        if (viewN > 0) {
            viewN -= 1;
            ViewInt(viewN);
        }
    }
    public void ViewRight() {
        if (viewN < toads.Count - 1) {
            viewN += 1;
            ViewInt(viewN);
        }
    }
    public void ViewInt(int j) {
        foreach (var i in toads) {
            i.ar1.SetActive(false);
            i.ar2.SetActive(false);
        }
        toads[j].ar1.SetActive(true);
        toads[j].ar2.SetActive(true);

        foreach (var i in toads) {
            i.cam.Priority = -9999;
        }
        toads[j].cam.Priority = 9999;
    }
    public void SelectToad(int j) {
        if (PlayerPrefs.GetInt("current_weight") > toads[j].max_weight_lvl[toads[j].toad_level] && j != 0) {
            if (fin_setup == false) return;
            GameObject.FindGameObjectWithTag("Popup").GetComponent<Popup>().show(
            "Not enough place in this toad inventory :(",
            "You can upgrade this toad or use another with more place in inventory"
            );
            return;
        }
        selectedN = j;
        PlayerPrefs.SetInt("SelectedToad", j);
        PlayerPrefs.SetInt("MaxWeight", toads[j].max_weight_lvl[toads[j].toad_level]);

        foreach (var i in toads) {
            i.staT.SetText("(tap on toad to select)");
        }
        toads[j].staT.SetText("selected");
        zsoul.selectToad(j);

        zh.canDoubleJump = false;
        zh.ignoreHead = false;
        zh.hpMaxToad = 0;
        zh.rotMod = 1;
        zh.canFall = false;
        zh.gameObject.GetComponent<CoinCollector>().coinMult = 1;

        if (selectedN == 0) {
            //
        } else if (selectedN == 1) {
            zh.hpMaxToad = 2;
        } else if (selectedN == 2) {
            zh.gameObject.GetComponent<CoinCollector>().coinMult *= 3;
            zh.canDoubleJump = true;
        } else if (selectedN == 3) {
            zh.canDoubleJump = true;
        } else if (selectedN == 4) {
            zh.ignoreHead = true;
            zh.hpMaxToad = 1;
        } else if (selectedN == 5) {
            zh.canDoubleJump = true;
            zh.rotMod = 1.5f;
            zh.gameObject.GetComponent<CoinCollector>().coinMult *= 2;
        } else if (selectedN == 6) {
            zh.canDoubleJump = true;
            zh.ignoreHead = true;
            zh.rotMod *= 1.5f; ;
            zh.canFall = true;
            zh.hpMaxToad = 3;
            zh.gameObject.GetComponent<CoinCollector>().coinMult *= 4;
        }
        SetHatBonus();
        zh.RestoreLives();
    }
    public void LockToad(int j) {
        toads[j].locker.SetActive(true);
        PlayerPrefs.SetInt("ToadLock" + j.ToString(), 1);
        if (selectedN == j) {
            selectedN = 0;
        }
    }
    public void UnlockToad(int j) {
        toads[j].locker.SetActive(false);
        PlayerPrefs.SetInt("ToadLock" + j.ToString(), 0);
        SelectToad(j);
    }
    public void SetHat(int j) {
        selectedHat = j;
        PlayerPrefs.SetInt("SelectedHat", j);

        SelectToad(selectedN);
    }
    void SetHatBonus() {
        int j = selectedHat;
        zh.hpMaxHat = 0;
        zsoul.selectHat(selectedHat);

        foreach (var i in hatDA) {
            i.SetActive(false);
        }

        if (j != -1) hatDA[j].SetActive(true);
        hatNET.SetActive(j == -1);

        if (selectedHat == 0) {
            zh.rotMod *= 1.5f; ;
        } else if (selectedHat == 1) {
            zh.hpMaxHat += 2;
        } else if (selectedHat == 2) {
            zh.hpMaxHat += 2;
        } else if (selectedHat == 3) {
            zh.ignoreHead = true;
        } else if (selectedHat == 4) {
            zh.gameObject.GetComponent<CoinCollector>().coinMult *= 2;
        } else if (selectedHat == 5) {
            zh.canFall = true;
        }
    }
}

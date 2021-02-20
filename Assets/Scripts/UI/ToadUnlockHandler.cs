using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToadUnlockHandler : MonoBehaviour
{
    public bool isLock;
    public int[] upgrade_cost;
    public int[] max_weight_lvl;
    public int toad_level;
    public int index;
    public int coins_need;
    public int level_need;
    public TMPro.TextMeshProUGUI coins, level;
    public GameObject locker;
    public Cinemachine.CinemachineVirtualCamera cam;
    public TMPro.TextMeshProUGUI staT;
    public TMPro.TextMeshProUGUI toad_level_text, upgrade_cost_text;
    public GameObject ar1;
    public GameObject ar2;

    GameObject zh;
    //UnlockRequirements un;
    private void Awake() {
        zh = GameObject.FindGameObjectWithTag("Player");
        if(PlayerPrefs.HasKey("ToadLvl" + index.ToString()) == false) {
            PlayerPrefs.SetInt("ToadLvl" + index.ToString(), 1);
        }
        toad_level = PlayerPrefs.GetInt("ToadLvl" + index.ToString());
        //un = GameObject.FindGameObjectWithTag("ZhSelect").GetComponent<ZhabaSelector>().unlockCost[index];
    }
    public void TryUnlock() {
        if (zh.GetComponent<CoinCollector>().GetCoins() >= coins_need) {
            if (zh.GetComponent<Levels>().level >= level_need) {
                unlock();
                zh.GetComponent<CoinCollector>().AddCoins(-coins_need);
            }
        }
    }
    public void unlock() {
        Debug.Log("FA");
        GameObject.FindGameObjectWithTag("ZhSelect").GetComponent<ZhabaSelector>().UnlockToad(index);
    }
    private void Update() {
        coins.SetText(zh.GetComponent<CoinCollector>().GetCoins().ToString() + " / " + coins_need.ToString() + " coins");
        level.SetText(zh.GetComponent<Levels>().level.ToString() + " / " + level_need.ToString() + " level");
        
        if (zh.GetComponent<CoinCollector>().GetCoins() < coins_need) 
            coins.color = new Color(0.5f, 0.5f, 0.5f);
        else 
            coins.color = new Color(1, 1, 1);

        if (zh.GetComponent<Levels>().level < level_need)
            level.color = new Color(0.5f, 0.5f, 0.5f);
        else
            level.color = new Color(1, 1, 1);

        toad_level_text.SetText("Level " + toad_level.ToString());
        upgrade_cost_text.SetText(toad_level >= upgrade_cost.Length - 1 ? "max":upgrade_cost[toad_level + 1].ToString());
    }
    public void tryUpgrade() {
        if (toad_level >= upgrade_cost.Length - 1) return;
        if (zh.GetComponent<CoinCollector>().GetCoins() >= upgrade_cost[toad_level + 1]) {
            zh.GetComponent<CoinCollector>().AddCoins(-upgrade_cost[toad_level + 1]);
            toad_level += 1;
            PlayerPrefs.SetInt("ToadLvl" + index.ToString(), toad_level);
        }
    }
    public void getUpgradeInfo() {
        GameObject.FindGameObjectWithTag("Popup").GetComponent<Popup>().show(
            "Your toad is now at level " + toad_level.ToString(),
            "Max carry weight: " + max_weight_lvl[toad_level].ToString() +
            (toad_level >= upgrade_cost.Length - 1 ? " (max)" : " (+" + (max_weight_lvl[toad_level + 1] - max_weight_lvl[toad_level]).ToString() + " if upgrade)") + "\n");
    }
}

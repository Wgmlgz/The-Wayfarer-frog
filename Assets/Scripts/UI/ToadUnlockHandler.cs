using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToadUnlockHandler : MonoBehaviour
{
    public int index;
    public TMPro.TextMeshProUGUI coins, level;
    GameObject zh;
    UnlockRequirements un;
    private void Awake() {
        zh = GameObject.FindGameObjectWithTag("Player");
        un = GameObject.FindGameObjectWithTag("ZhSelect").GetComponent<ZhabaSelector>().unlockCost[index];
    }
    public void TryUnlock() {
        if (zh.GetComponent<CoinCollector>().GetCoins() >= un.coins) {
            if (zh.GetComponent<Levels>().level >= un.level) {
                GameObject.FindGameObjectWithTag("ZhSelect").GetComponent<ZhabaSelector>().UnlockToad(index);
                zh.GetComponent<CoinCollector>().AddCoins(-un.coins);
            }
        }
    }
    private void Update() {
        coins.SetText(zh.GetComponent<CoinCollector>().GetCoins().ToString() + " / " + un.coins.ToString() + " coins");
        level.SetText(zh.GetComponent<Levels>().level.ToString() + " / " + un.level.ToString() + " level");
        
        if (zh.GetComponent<CoinCollector>().GetCoins() < un.coins) 
            coins.color = new Color(0.5f, 0.5f, 0.5f);
        else 
            coins.color = new Color(1, 1, 1);

        if (zh.GetComponent<Levels>().level < un.level)
            level.color = new Color(0.5f, 0.5f, 0.5f);
        else
            level.color = new Color(1, 1, 1);
    }
}

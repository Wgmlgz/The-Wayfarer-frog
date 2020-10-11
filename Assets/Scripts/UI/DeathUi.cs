using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeathUi : MonoBehaviour
{
    public int slCost = 50;
    const float bigLineSize = 225f;
    
    [Range(0f, 1f)] public float[] lnVals;
    public GameObject[] lines;
    
    public ZhabaController ZH;
    public TMPro.TextMeshProUGUI textBSL;
    public TMPro.TextMeshProUGUI textUseSL;

    private void Update() {
        PrepareDeathScreen();
        for(int i = 0; i < lines.Length; ++i) {
            lines[i].transform.localPosition =  new Vector2(((lnVals[i] * 2) -1f) * bigLineSize, 0);
        }
    }
    public void SetLine(int n, float val){
        lnVals[n] = val;
    }

    public void PrepareDeathScreen(){
        textBSL.SetText("Buy\n(" + slCost + " coins)");
        textUseSL.SetText("Use\n(" + ZH.secondLifes + " left)");
    }

    public void BuyLife(){
        if(ZH.GetComponent<CoinCollector>().GetCoins() >= slCost){
            ZH.secondLifes += 1;
            PlayerPrefs.SetInt("SL", ZH.secondLifes);
            ZH.GetComponent<CoinCollector>().AddCoins(-slCost);
        }
        PrepareDeathScreen();
    }

    public void UseSeconLife(){
        if(ZH.secondLifes > 0){
            ZH.secondLifes -= 1;
            PlayerPrefs.SetInt("SL", ZH.secondLifes);
            ZH.Continue();
            PrepareDeathScreen();
        }
    }
}

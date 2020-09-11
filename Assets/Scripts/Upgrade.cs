using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public string saveName = "idName";
    public int stage = -1;
    public int maxStage = 10;
    public float[] values;
    public int[] costs;
    public GameObject[] boxes;
    public TMPro.TextMeshProUGUI btnSt;

    [Header("Using")]
    public bool forRS;

    public ZhabaController ZH;
    public CoinCollector CN;
    private void Start() {
        SetBoxes();
        ZH = GameObject.FindGameObjectWithTag("Player").GetComponent<ZhabaController>();
        CN = ZH.gameObject.GetComponent<CoinCollector>();
        SetName();
    }
    void SetBonus(){
        if(forRS){
            ZH.rotationSpeed = values[stage];
        }
    }
    void SetBoxes(){
        for(int i = 0; i < maxStage; ++i){
            if(i <= stage){
                boxes[i].SetActive(true);
            }else{
                boxes[i].SetActive(false);
            }
        }
    }
    void SetName(){
        btnSt.SetText("Buy " + ((stage == maxStage - 1)?"(Max)" : "( " + (costs[stage + 1].ToString() + " coins)")));
    }
    public void Buy(){
        if(stage < maxStage - 1){
            if(CN.coins >= costs[stage + 1]){
                CN.coins -= costs[stage + 1];
                stage += 1;
                PlayerPrefs.SetInt(saveName, stage);
                SetBoxes();
                SetName();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public bool DBG;
    public string saveName = "idName";
    public int stage = -1;
    public int maxStage = 10;
    public float[] values;
    public int[] costs;
    public GameObject[] boxes;
    public TMPro.TextMeshProUGUI btnSt;
    public GameObject maxGO;

    [Header("Using")]
    public bool forRS;
    public bool forHP;
    public bool forAC;

    private ZhabaController ZH;
    private CoinCollector CN;

    private void Awake() {
        
    }
    private void Start() {
        ZH = GameObject.FindGameObjectWithTag("Player").GetComponent<ZhabaController>();
        CN = ZH.gameObject.GetComponent<CoinCollector>();

        if(PlayerPrefs.HasKey(saveName) == false){
            PlayerPrefs.SetInt(saveName, -1);
        }
        stage = PlayerPrefs.GetInt(saveName);

        SetBoxes();
        SetName();
        SetBonus();
    }
    void SetBonus(){
        float val = values[stage + 1];
        int valInt = Mathf.RoundToInt (val);
        if(forRS){
            ZH.rotationSpeed = val;
        }
        if (forHP) {
            ZH.hpMax = valInt;
        }
        if (forAC) {
            ZH.speedUp = val;
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
        maxGO.SetActive(stage == maxStage - 1);
    }
    void SetName(){
        btnSt.SetText("Buy " + ((stage == maxStage - 1)?"(Max)" : "( " + (costs[stage + 1].ToString() + " coins)")));
    }
    public void Buy(){
        if(stage < maxStage - 1){
            if(CN.coins >= costs[stage + 1]){
                CN.ChangeCoins(-costs[stage + 1]);
                stage += 1;
                PlayerPrefs.SetInt(saveName, stage);
                SetBoxes();
                SetName();
            }
        }
    }
    private void Update() {
        if(DBG){
            SetBonus();
        }
    }
}

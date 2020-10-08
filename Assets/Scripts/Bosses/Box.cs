using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public ZhabaController ZH;
    public TileGenerator TG;
    public UIManager UM;

    public int reward = 500;

    public bool isMain = true;

    public void EnterBox() {
        transform.SetParent(null);
        GetComponent<Animation>().Play();

        ZH.doConstVelosity = true;
        ZH.constVelosity = 0;

        TG.ChangeBiom (1);
        ZH.SetToGod();
        Invoke("MoveZhaba", 3);
        //Invoke("StartFight", 4);
    }

    void MoveZhaba() {
        ZH.doConstVelosity = true;
        ZH.constVelosity = 150;
        UM.SetRedText("The box.");
    }

    public void StartFight() {
        ZH.SetToGod(false);
        ZH.doConstVelosity = false;

        ZH.doConstMaxSpeed = true;
        ZH.constMaxSpeed = 10;

        UM.HedeRedText();
    }

    public void EndFight() {
        UM.SetRedText("Win");
        ZH.doConstMaxSpeed = false;
        ZH.gameObject.GetComponent<CoinCollector>().AddCoins(reward);
        PlayerPrefs.SetInt("SBox", 1);
        Invoke("EndEndFight", 3);
    }

    public void EndEndFight() {
        UM.HedeRedText();
    }
}
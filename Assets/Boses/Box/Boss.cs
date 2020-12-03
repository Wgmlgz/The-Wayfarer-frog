using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
    public string boss_name = "The box.";
    public ZhabaController ZH;
    public TileGenerator TG;
    public UIManager UM;

    public int reward = 500;

    public bool isMain = true;

    public void EnterBoss() {
        transform.SetParent(null);
        GetComponent<Animation>().Play();

        ZH.doConstVelosity = true;
        ZH.constVelosity = 0;

        if (boss_name == "The box.")
          TG.ChangeBiom(1);
        if (boss_name == "Pepe the frog.")
            TG.ChangeBiom(4);
        ZH.SetToGod();
        Invoke("MoveZhaba", 3);
        //Invoke("StartFight", 4);
    }

    void MoveZhaba() {
        ZH.doConstVelosity = true;
        ZH.constVelosity = 150;
        UM.SetRedText(boss_name);
    }

    public void StartBoxFight() {
        ZH.SetToGod(false);
        ZH.doConstVelosity = false;

        ZH.doConstMaxSpeed = true;
        ZH.constMaxSpeed = 10;

        UM.HedeRedText();
    }
    public void StartPepeFight() {
        ZH.SetToGod(false);
        ZH.doConstVelosity = false;

        UM.HedeRedText();

        GetComponent<Pepe>().StartFight();
    }


    public void EndBoxFight() {
        UM.SetRedText("Win");
        ZH.doConstMaxSpeed = false;
        ZH.gameObject.GetComponent<CoinCollector>().AddCoins(reward);
        PlayerPrefs.SetInt("SBox", 1);
        Invoke("EndEndFight", 3);
    }
    public void EndPepeFight() {
        UM.SetRedText("Win");
        Debug.Log(gameObject);
        GetComponent<Pepe>().EndFight();
        ZH.gameObject.GetComponent<CoinCollector>().AddCoins(reward);
        PlayerPrefs.SetInt("SPepe", 1);
        Invoke("EndEndFight", 3);
    }

    public void EndEndFight() {
        UM.HedeRedText();
    }
}
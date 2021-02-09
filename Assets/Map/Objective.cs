using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour {
    public TMPro.TextMeshProUGUI text;
    
    private void Start() {
        if (PlayerPrefs.HasKey("current_objective") == false) PlayerPrefs.SetString("current_objective", "Come to Qrerton");
        string s = PlayerPrefs.GetString("current_objective");
        if (PlayerPrefs.GetInt("Help my friends_completed") != 0) {
            if (s == "Help Pepe the Frog's friends in: Zing, Big Floppa, Flark, Nand and Zlork") {
                
                PlayerPrefs.SetString("current_objective", "Return to Pepe the Frog");
                s = "Return to Pepe the Frog";
            }
        }
        if (s.Length > 30) {
            s = s.Insert(29, "\n");
        }
        text.SetText("Your current objective: " + s);
    }
}

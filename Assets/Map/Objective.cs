using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour {
    public TMPro.TextMeshProUGUI text;
    
    private void LateUpdate() {
        if (PlayerPrefs.HasKey("current_objective") == false) PlayerPrefs.SetString("current_objective", "Come to Qrerton");
        text.SetText("Your current objective: " + PlayerPrefs.GetString("current_objective"));
    }
}

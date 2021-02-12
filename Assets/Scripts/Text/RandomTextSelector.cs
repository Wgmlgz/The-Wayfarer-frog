using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTextSelector : MonoBehaviour {
    [Header("Const")]
    public bool useConst;
    public string CONST_VAL;

    [Header("Var")]
    public bool useVar;
    public string varVal;

    [Header("Random")]
    public int usedVariation;
    public List<string> variations;

    public void SetToConst(string s) {
        useConst = true;
        CONST_VAL = s;
    }
    public void ChangeText(string newStr = "") {
        if (newStr != "") {
            useVar = true;
            varVal = newStr;
            SetStr(varVal);
        } else if (useConst) {
            SetStr(CONST_VAL);
        } else if (useVar) {
            SetStr(PlayerPrefs.GetString(varVal));
        } else {
            usedVariation = Random.Range(0, variations.Count);
            SetStr(variations[usedVariation]);
        }
    }
    public void SetStr(string s) {
        GetComponent<TMPro.TextMeshProUGUI>().SetText(s);
    }

    private void Start() {
        ChangeText();
    }
}

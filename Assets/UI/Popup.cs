using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour {
    public TMPro.TextMeshProUGUI title;
    public TMPro.TextMeshProUGUI description;

    private void Awake() {
        gameObject.GetComponent<Canvas>().enabled = false;
    }
    public void setTitle(string s) {
        title.SetText(s);
    }
    public void setDesctiption(string s) {
        description.SetText(s);
    }
    public void show() {
        gameObject.GetComponent<Canvas>().enabled = true;
    }
    public void show(string title, string description) {
        setTitle(title);
        setDesctiption(description);
        show();
    }
}

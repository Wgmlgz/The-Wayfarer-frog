using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PopupData {
    public string title;
    public string description;
    public PopupData(string ntitle, string ndescription) {
        title = ntitle;
        description = ndescription;
    }
}
public class Popup : MonoBehaviour {
    public TMPro.TextMeshProUGUI title_T;
    public TMPro.TextMeshProUGUI description_T;
    PopupData tmp_PopupData;
    Queue<PopupData> q;
    bool is_displaying;
    private void Awake() {
        gameObject.GetComponent<Canvas>().enabled = false;
        tmp_PopupData = new PopupData("", "");
        q = new Queue<PopupData>();
    }
    public void setTitle(string s) {
        tmp_PopupData.title = s;
    }
    public void setDesctiption(string s) {
        tmp_PopupData.description = s;
    }
    public void show() {
        show(tmp_PopupData.title, tmp_PopupData.description);
    }
    public void show(string title, string description) {
        if (is_displaying) {
            q.Enqueue(new PopupData(title, description));
        }
        else {
            is_displaying = true;
            title_T.SetText(title);
            description_T.SetText(description);
            gameObject.GetComponent<Canvas>().enabled = true;
        }
    }
    public void okClick() {
        is_displaying = false;
        if (q.Count == 0) { return; }
        
        var t = q.Dequeue();
        show(t.title, t.description);
    }
}

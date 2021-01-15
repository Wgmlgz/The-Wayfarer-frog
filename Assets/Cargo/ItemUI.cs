using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
[System.Serializable]
public class namedImg {
    public string name;
    public Sprite img;
}
public class ItemUI : MonoBehaviour {
    public List<namedImg> imgs;
    public Item item;

    public TMPro.TextMeshProUGUI name_text;
    public TMPro.TextMeshProUGUI weight_text;
    public TMPro.TextMeshProUGUI target_text;
    public UnityEngine.UI.Image img;

    public void refreshTexts() {
        name_text.SetText(item.name);
        weight_text.SetText(item.weight.ToString() + " kg");
        target_text.SetText("target: " + item.target.ToString());
        if (item.is_in_task == false) { target_text.SetText(""); }
        foreach (var i in imgs) {
            if (i.name == item.img_name) {
                Debug.Log("da");
                img.sprite = i.img;
                break;
            }
        }
    }
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}

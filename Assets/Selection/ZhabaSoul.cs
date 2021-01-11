using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUtil {
    public static void selectOne(List<GameObject> list, int active) {
        foreach (var i in list) i.SetActive(false);
        if (active == -1) return;
        list[active].SetActive(true);
    }
}
public class ZhabaSoul : MonoBehaviour {
    public int selected_toad;
    public List<GameObject> souls;
    public bool auto_start;

    public void selectToad(int id) {
        selected_toad = id;
        SelectUtil.selectOne(souls, id);
    }
    public void selectHat(int id) {
        souls[selected_toad].GetComponent<Soul>().selectHat(id);
    }
    void Start() {
        if(auto_start) {
            selectToad(PlayerPrefs.GetInt("SelectedToad"));
            selectHat(PlayerPrefs.GetInt("SelectedHat"));
        }
    }

    // Update is called once per frame
    void Update() {

    }
}

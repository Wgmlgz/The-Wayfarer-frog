using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskList : MonoBehaviour {
    string city;
    public GameObject example;
    public GameObject parent;
    public float offset;
    List<GameObject> ui_tasks = new List<GameObject>();
    void Start() {
        city = PlayerPrefs.GetString("CurrentCity");
        var loaded_tasks = (List<TaskData>)ItemsListUtility.loadObj(city + "_tasks");
        foreach (var i in loaded_tasks) {
            GameObject tmp = Instantiate(example, parent .transform);

            tmp.GetComponent<RectTransform>().localPosition =
                new Vector2(0, ui_tasks.Count * - offset);

            tmp.GetComponent<TaskUI>().task = i;
            tmp.GetComponent<TaskUI>().updateTexts();
            ui_tasks.Add(tmp);
        }
    }

    void Update() {
        parent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, ui_tasks.Count * offset);
    }
}

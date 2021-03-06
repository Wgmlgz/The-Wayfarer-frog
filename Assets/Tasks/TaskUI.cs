﻿using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TaskData {
    public enum TaskType {
        delivery,
        do_tasks,
        in_do_tasks,
    }
    public enum TaskStatus {
        active,
        complited
    }

    public string task_name;
    public string task_description;


    public TaskStatus status;
    public TaskType type;

    [Header("Delivery")]
    public string from;
    public string target;

    public List<Item> items;
    public bool not_spawn;

    [Header("Do other tasks")]
    public int tasks_left;

    [Header("In do other tasks")]
    public string parent_city;
    public string parent_task;

    [Header("All")]

    public int revard;
    public List<Item> reward_items;
}

public class TaskUI : MonoBehaviour {
    public TaskData task;
    public TMPro.TextMeshProUGUI from_text;
    public TMPro.TextMeshProUGUI target_text;

    public TMPro.TextMeshProUGUI reward_text;
    public TMPro.TextMeshProUGUI weight_text;

    public TMPro.TextMeshProUGUI header_text;
    public TMPro.TextMeshProUGUI description_text;
    

    public void updateTexts() {
        from_text.SetText(task.from);
        target_text.SetText(task.target);
        reward_text.SetText(task.revard.ToString());
        weight_text.SetText(ItemsListUtility.calcTotalWeight(task.items).ToString());

        header_text.SetText(task.task_name);
        string items = "\nItems to deliver: ";
        for (int i = 0; i < task.items.Count; ++i) {
           if (i != 0) items += ", ";
           items += task.items[i].name;
        }
        items += "";
        description_text.SetText(task.task_description + items);
    }
    void Start() {
        updateTexts();
    }

    // Update is called once per frame
    void Update() {

    }
}

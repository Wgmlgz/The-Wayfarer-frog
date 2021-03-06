﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour {
    public List<City> connected;
    public List<City> new_connected;
    public string do_connect;
    public TMPro.TextMeshProUGUI tasks_count_text;
    public List<TaskData> default_tasks;
    public List<TaskData> tasks;

    public List<Item> default_items;
    public List<Item> items;
    bool load_success = true;
    GameObject map_u;

    public static int getDistance(City a, City b, float da = 50) {
        int ret;
        var delta = a.GetComponent<RectTransform>().position -
            b.GetComponent<RectTransform>().position;
        ret = Mathf.RoundToInt(delta.magnitude * da);
        return ret;
    }
    public void addItemsFromTasks(List<TaskData> n_tasks) {
        foreach (var i in n_tasks) {
            if (i.not_spawn == true) continue;
            foreach (var j in i.items) {
                j.from = i.from;
                j.target = i.target;
                j.is_in_task = true;
                j.task_name = i.task_name;
                items.Add(j);
            }
        }
    }
    public void saveDefault() {
        Debug.Log("Using default stuff in: " + name);
        items = default_items;
        tasks = default_tasks;
        addItemsFromTasks(default_tasks);

        saveData();
        load_success = false;
    }
    public void saveData() {
        ItemsListUtility.saveListObj(items, name);
        ItemsListUtility.saveListObj(tasks, name + "_tasks");
    }
    private void Awake() {
        load_success = true;
        map_u = GameObject.FindGameObjectWithTag("MapU");
        var loaded_items = ItemsListUtility.loadList(name);
        if (loaded_items == null) {
            saveDefault();
        } else {
            items = loaded_items;
            var loaded_tasks = (List<TaskData>)ItemsListUtility.loadObj(name + "_tasks");
            tasks = loaded_tasks;
            addItemsFromTasks(tasks);
        }
        if(name == PlayerPrefs.GetString("CurrentCity")) {
            map_u.GetComponent<MapU>().cur_city = this;
            scopeSelf();
        }
        if (name == PlayerPrefs.GetString("TargetCity")) {
            map_u.GetComponent<MapU>().target_city = this;
            map_u.GetComponent<ImgLine>().target = gameObject.GetComponent<RectTransform>();
            var d = getDistance(this, map_u.GetComponent<MapU>().cur_city);
            //PlayerPrefs.SetInt("DistanceToTargetCity", d);
            map_u.GetComponent<MapU>().distance_text.SetText(d.ToString() + " m.");
        }
        tasks_count_text.SetText(tasks.Count.ToString());
        if (tasks.Count == 0) tasks_count_text.SetText("");
    }
    private void Start() {
        if (name == PlayerPrefs.GetString("CurrentCity")) {
            scopeSelf();
        }
    }
    public void scopeSelf() {
        map_u.GetComponent<MapU>().map.anchoredPosition
            = -GetComponent<RectTransform>().localPosition;

    }
    public void targetSelf() {
        if (map_u.GetComponent<MapU>().cur_city != this) {
            if (map_u.GetComponent<MapU>().cur_city.connected.Contains(this) == false) {
                return;
            }
            map_u.GetComponent<MapU>().target_city = this;
            map_u.GetComponent<ImgLine>().target = gameObject.GetComponent<RectTransform>();
            PlayerPrefs.SetString("TargetCity", name);
            var d = getDistance(this, map_u.GetComponent<MapU>().cur_city);
            PlayerPrefs.SetInt("DistanceToTargetCity", d);
            map_u.GetComponent<MapU>().distance_text.SetText(d.ToString() + " m.");
        }
    }
    public void tpSelf() {
        map_u.GetComponent<MapU>().cur_city = this;
        PlayerPrefs.SetString("CurrentCity", name);
        scopeSelf();
    }
}

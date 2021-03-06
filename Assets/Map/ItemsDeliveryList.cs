﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsDeliveryList : MonoBehaviour {
    public string inventory_name;
    public GameObject example;
    public GameObject parent;
    public Path path;
    public float offset;
    List<GameObject> list_items = new List<GameObject>();
    public GameObject to_disable;
    void Start() {
        var loaded_tasks = ItemsListUtility.loadList(inventory_name);
        bool b = true;
        if (loaded_tasks != null) {
            foreach (var i in loaded_tasks) {
                if (i.is_in_task == false) continue;
                b = false;
                GameObject tmp = Instantiate(example, parent.transform);

                tmp.GetComponent<RectTransform>().localPosition =
                    new Vector2(0, list_items.Count * -offset);

                tmp.GetComponent<ItemUI>().item = i;
                tmp.GetComponent<ItemUI>().refreshTexts();
                list_items.Add(tmp);
                PathTile tmp_tile = new PathTile();
                var da = Object.FindObjectsOfType<City>();
                foreach (var j in da) {
                    if (j.name == tmp.GetComponent<ItemUI>().item.target) {
                        tmp_tile.target = j.gameObject.GetComponent<RectTransform>();
                    }
                    if (j.name == PlayerPrefs.GetString("CurrentCity")) {
                        tmp_tile.from = j.gameObject.GetComponent<RectTransform>();
                    }
                }
                if (path != null) path.tiles.Add(tmp_tile);
            }
        }
        if (b) to_disable.SetActive(false);
        if (path != null) path.createPath();
    }

    void Update() {
        parent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, list_items.Count * offset);
    }
}

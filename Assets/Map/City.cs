using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour {
    public List<TaskData> default_tasks;
    public List<TaskData> tasks;

    public List<Item> default_items;
    public List<Item> items;
    GameObject map_u;

    void addItemsFromTasks(List<TaskData> n_tasks) {
        foreach (var i in n_tasks) {
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
        
        ItemsListUtility.saveListObj(items, name);
        ItemsListUtility.saveListObj(tasks, name + "_tasks");
    }
    private void Awake() {
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
    }
    
    public void targetSelf() {
        if (map_u.GetComponent<MapU>().cur_city != this)
            map_u.GetComponent<ImgLine>().target = gameObject.GetComponent<RectTransform>(); 
    }
    public void tpSelf() {
        map_u.GetComponent<MapU>().cur_city = this;
    }
}

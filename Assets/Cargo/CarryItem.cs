using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
[System.Serializable]
public class Item {
    public string name = "Default name";
    public int weight = 10;

    public string img_name;

    [HideInInspector] public string from;
    [HideInInspector] public string target;

    [HideInInspector] public bool is_in_task;
    [HideInInspector] public string task_name;

    public static bool isEqual(Item a, Item b) {
        if (a.name != b.name) return false;
        if (a.weight != b.weight) return false;

        if (a.from != b.from) return false;
        if (a.target != b.target) return false;

        if (a.is_in_task != b.is_in_task) return false;
        if (a.task_name != b.task_name) return false;
        return true;
    }
}

public class CarryItem : MonoBehaviour {
    public Item item;

    public ItemUI item_ui;
    public WeightList list;

    public void moveToFirst() {
        GameObject tmp = list.CreateFirst();
        list.setProperties(tmp.GetComponent<CarryItem>(), item);
        list.list_second.Remove(gameObject);
        Destroy(gameObject);

        list.saveAll();
    }
    public void moveToSecond() {
        string cur_city = PlayerPrefs.GetString("CurrentCity");
        if (item.target == cur_city && item.is_in_task) {
            

            var from_city_tasks =
                (List<TaskData>)ItemsListUtility.loadObj(item.from + "_tasks");
            foreach (var tmp_task in from_city_tasks) {
                foreach (var tmp_item in tmp_task.items) {
                    if (Item.isEqual(tmp_item, item)) {
                        Debug.Log("You delivered " + item.name + " from " +
                            tmp_task.from + " to " + tmp_task.target +
                            " in task: " + tmp_task.task_name);
                        tmp_task.items.Remove(tmp_item);
                        if (tmp_task.items.Count == 0) {
                            list.collector.AddCoins(tmp_task.revard);
                            Debug.Log("You delivered " + tmp_task.task_name + "!");
                            GameObject.FindGameObjectWithTag("Popup").GetComponent<Popup>().show(
                                "You have completed the task!",
                                "You delivered " + item.name + " from " +
                                tmp_task.from + " to " + tmp_task.target +
                                " in task: " + tmp_task.task_name + "\n" +
                                "And earned " + tmp_task.revard + " coins!"
                            );
                        }
                        from_city_tasks.Remove(tmp_task);
                        ItemsListUtility.saveListObj(from_city_tasks, item.from + "_tasks");
                        goto Da;
                    } else {
                        Debug.Log("Nope(" + item.name + "/" + tmp_task.items[0].name);
                    }
                }
            }
        } else {
            GameObject tmp = list.CreateSecond();
            list.setProperties(tmp.GetComponent<CarryItem>(), item);
        }

    Da:
        list.list_first.Remove(gameObject);
        Destroy(gameObject);

        list.saveAll();
    }
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}

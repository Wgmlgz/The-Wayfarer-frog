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
    public void finTask(TaskData tmp_task) {
        list.collector.AddCoins(tmp_task.revard);
        Debug.Log("You delivered " + tmp_task.task_name + "!");
        GameObject.FindGameObjectWithTag("Popup").GetComponent<Popup>().show(
            "Nice work, oleg!",
            "You have completed the task: " + tmp_task.task_name + "\n" +
            (tmp_task.type.Equals(TaskData.TaskType.do_tasks) ? "For additional revard go to " + tmp_task.from + "\n": "") +
            "And earned " + tmp_task.revard + " coins!"
        );
        PlayerPrefs.SetInt(tmp_task.task_name + "_completed", 1);
        if (tmp_task.type.Equals(TaskData.TaskType.in_do_tasks)) {
            Debug.LogWarning("dfsdfsdfs");
            var parent_city_tasks =
                (List<TaskData>)ItemsListUtility.loadObj(tmp_task.parent_city + "_tasks");
            foreach (var task_in_parent_city in parent_city_tasks) {
                if(task_in_parent_city.task_name == tmp_task.parent_task) {
                    --task_in_parent_city.tasks_left;
                    if (task_in_parent_city.tasks_left == 0) {
                        Debug.Log("You done task: " + task_in_parent_city.task_name);
                        finTask(task_in_parent_city);
                        parent_city_tasks.Remove(task_in_parent_city);
                        return;
                    }
                    ItemsListUtility.saveListObj(parent_city_tasks, tmp_task.parent_city + "_tasks");
                }
            }
        }
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
                            finTask(tmp_task);
                            from_city_tasks.Remove(tmp_task);
                        }
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

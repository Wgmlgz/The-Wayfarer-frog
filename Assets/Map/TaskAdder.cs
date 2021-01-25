using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskAdder : MonoBehaviour {
    public City city;
    public List<TaskData> tasks;

    public void addTasks() {
        city.tasks.AddRange(tasks);
        city.addItemsFromTasks(tasks);
        city.saveData();
    }
}

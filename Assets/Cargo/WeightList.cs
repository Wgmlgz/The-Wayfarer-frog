using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ItemsListUtility {
    // save & load
    public static void saveListObj(object list, string name) {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + name + ".save");
        bf.Serialize(file, list);
        file.Close();
        Debug.Log("List saved! (" + (Application.persistentDataPath + "/" + name + ".save") + ")");
    }
    public static void saveList(List<GameObject> list, string name) {
        List<Item> tmp_items = new List<Item>();
        foreach (var i in list) {
            tmp_items.Add(i.GetComponent<CarryItem>().item);
        }
        saveListObj(tmp_items, name);
    }

    public static object loadObj(string name) {
        if (File.Exists(Application.persistentDataPath + "/" + name + ".save")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + name + ".save", FileMode.Open);
            object save = bf.Deserialize(file);
            file.Close();
            Debug.Log("List Loaded (" + (Application.persistentDataPath + "/" + name + ".save") + ")");
            return save;
        } else {
            Debug.Log("No list saved! (" + name + ")");
        }
        return null;
    }

    public static List<Item> loadList(string name) {
        var save = loadObj(name);
        if (save == null) {
            return null;
        } else {
            var da = (List<Item>)save;

            return (List<Item>)save;
        }
    }


    // calc
    public static int calcTotalWeight(List<Item> list) {
        int tmp_weight = 0;
        for (int i = 0; i < list.Count; ++i) tmp_weight += list[i].weight;
        return tmp_weight;
    }
}
public class WeightList : MonoBehaviour {
    public int max_weight_first;
    public int max_weight_second;

    public Color color_ok;
    public Color color_bad;

    public GameObject content_first;
    public GameObject content_second;

    public GameObject example_first;
    public GameObject example_second;

    public List<GameObject> list_first;
    public List<GameObject> list_second;

    public TMPro.TextMeshProUGUI weight_obj_first;
    public TMPro.TextMeshProUGUI weight_obj_second;

    public GameObject addItem(GameObject parent, GameObject example, bool is_first, float offset = 75) {
        float length = example.GetComponent<RectTransform>().rect.width;
        GameObject tmp = Instantiate(example, parent.transform);
        tmp.SetActive(true);
        if (is_first) {
            list_first.Add(tmp);
        } else {
            list_second.Add(tmp);
        }
        return tmp;
    }
    void updateItemsPositions() {
        float offset = 75;
        int tmp_weight = 0;
        for (int i = 0; i < list_first.Count; ++i) {
            float length = list_first[i].GetComponent<RectTransform>().rect.width;
            list_first[i].GetComponent<RectTransform>().localPosition =
                new Vector2(length / 2, -i * offset);
            tmp_weight += list_first[i].GetComponent<CarryItem>().item.weight;
        }
        weight_obj_first.SetText(tmp_weight.ToString() + " / " + max_weight_first.ToString() + " kg");

        weight_obj_first.color = tmp_weight > max_weight_first ? color_bad : color_ok;

        tmp_weight = 0;
        for (int i = 0; i < list_second.Count; ++i) {
            float length = list_second[i].GetComponent<RectTransform>().rect.width;
            list_second[i].GetComponent<RectTransform>().localPosition =
                new Vector2(length / 2, -i * offset);
            tmp_weight += list_second[i].GetComponent<CarryItem>().item.weight;
        }
        weight_obj_second.SetText(tmp_weight.ToString() + " / " + max_weight_second.ToString() + " kg");

        weight_obj_second.color = tmp_weight > max_weight_second ? color_bad : color_ok;
    }
    public void setProperties(CarryItem obj, Item item) {
        obj.item = item;

        obj.item_ui.item = item;
        obj.item_ui.refreshTexts();
    }
    public void saveAll() {
        saveFirst("ZhabkaItems");
        saveSecond(PlayerPrefs.GetString("CurrentCity"));
    }
    public GameObject CreateFirst() {
        GameObject tmp = addItem(content_first, example_first, true);
        return tmp;
    }
    public GameObject CreateSecond() {
        GameObject tmp = addItem(content_second, example_second, false);
        return tmp;
    }

    public void CreateFirstNORET() {
        addItem(content_first, example_first, true);
    }
    public void CreateSecondNORET() {
        addItem(content_second, example_second, false);
    }

    public void saveFirst(string name) { ItemsListUtility.saveList(list_first, name); }
    public void saveSecond(string name) { ItemsListUtility.saveList(list_second, name); }

    //void reloadListList(<GameObject> list, string name)
    public void loadFirst(string name) {
        foreach (var i in list_first) Destroy(i);
        list_first.Clear();
        var loaded_items = ItemsListUtility.loadList(name);
        // if no zhabka inventory saved
        if (loaded_items == null) {
            ItemsListUtility.saveListObj(new List<Item>(), name);
            loaded_items = ItemsListUtility.loadList(name);
        }
        foreach (var i in loaded_items) {
            GameObject tmp = CreateFirst();
            setProperties(tmp.GetComponent<CarryItem>(), i);
        }
    }
    public void loadSecond(string name) {
        foreach (var i in list_second) Destroy(i);
        list_second.Clear();
        foreach (var i in ItemsListUtility.loadList(name)) {
            GameObject tmp = CreateSecond();
            setProperties(tmp.GetComponent<CarryItem>(), i);
        }
    }

    // helper
    public void toMap() {
        ScenesManager.toMapSt();
    }

    // MonoBehaviour
    void Start() {
        example_first.SetActive(false);
        example_second.SetActive(false);

        loadFirst("ZhabkaItems");
        loadSecond(PlayerPrefs.GetString("CurrentCity"));
    }
    void Update() {
        content_first.GetComponent<RectTransform>().sizeDelta = new Vector2(0, list_first.Count * 75);
        content_second.GetComponent<RectTransform>().sizeDelta = new Vector2(0, list_second.Count * 75);
        updateItemsPositions();
    }
}

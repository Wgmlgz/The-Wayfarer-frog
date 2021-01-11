using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PathTile {
    public RectTransform from;
    public RectTransform target;
}

public class Path : MonoBehaviour {
    public GameObject example;
    public Transform parent;
    public List<PathTile> tiles;

    public List<GameObject> tiles_obj;

    public void createPath() {
        tiles_obj.Clear();

        foreach(var i in tiles) {
            GameObject tmp = Instantiate(example, parent);
            tmp.SetActive(true);
            var line = tmp.GetComponent<ImgLine>();
            line.from = i.from;
            line.target = i.target;
            line.obj = tmp.GetComponent<RectTransform>();

            tiles_obj.Add(tmp);
        }
    }
    void Start() {
        example.SetActive(false);
        createPath();
    }

    // Update is called once per frame
    void Update() {

    }
}

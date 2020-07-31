using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Layer
{
    public string name;
    public float factor;
    public Color color;
    public Sprite[] textures;
    public List<GameObject> objs;
}

public class BackgroundGernerator : MonoBehaviour
{
    public Transform target;
    public float removeDistance;
    public Layer[] layers;
    public GameObject empty;

    //--
    //[Header("private")]

    private void Start()
    {
        foreach (var j in layers)
        {
            for (int i = 0; i < 10; i++)
            {
                GenNewTileInLayer(0);
            }
        }
    }
    public void GenNewTileInLayer(int i)
    {
        GameObject tmp = Instantiate(empty);
        
        Vector3 tmpPos = layers[i].objs[layers[i].objs.Count - 1].transform.position;
        tmpPos.x += removeDistance;
        tmp.transform.position = tmpPos;

        //layers[i].objs.Capacity += 1;
        //layers[i].objs.
        layers[i].objs.Add(tmp);
    }
    private void LateUpdate()
    {
        for (int i = 0; i < layers.Length; i++)
        {
            for (int j = 0; j < layers[i].objs.Count; j++)
            {
                if (target.position.x - layers[i].objs[j].transform.position.x > removeDistance)
                {
                    layers[i].objs.Remove(layers[i].objs[j]);
                    Destroy(layers[i].objs[j]);
                }
                if (j == layers[i].objs.Count -1 && target.position.x - layers[i].objs[j].transform.position.x < -removeDistance)
                {
                    GenNewTileInLayer(i);
                }
            }
        }
    }
}

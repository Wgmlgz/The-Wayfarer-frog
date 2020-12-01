using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo : MonoBehaviour {
    public bool isExample = true;
    public GameObject generator;
    public Vector3 tileLength;
    public GameObject ZH;
    public string biome_name;
    private void Awake() {
        ZH = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update() {
        if (isExample == false) {
            if (generator.GetComponent<TileGenerator>().Target.transform.position.x - tileLength.x - transform.position.x
                > generator.GetComponent<TileGenerator>().genDistance
                && gameObject != generator.GetComponent<TileGenerator>().lastTile) {
                Destroy(gameObject);
            }
            if (Mathf.Abs(generator.GetComponent<TileGenerator>().Target.transform.position.x - transform.position.x)
                < ZH.GetComponent<NPCManager>().min_dist) {
                ZH.GetComponent<NPCManager>().min_dist =
                    generator.GetComponent<TileGenerator>().Target.transform.position.x - transform.position.x;
                ZH.GetComponent<NPCManager>().place = biome_name;
            }
        }
    }
    private void LateUpdate() {
        
    }
}

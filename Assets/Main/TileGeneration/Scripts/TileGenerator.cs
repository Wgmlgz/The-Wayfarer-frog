using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Biom {
    public string name;
    public bool canGen;
    public int minBiomSize = 2;
    public int maxBiomSize = 5;
    public GameObject start_tile;
    public GameObject end_tile;
    public List<GameObject> tiles;
}

public class TileGenerator : MonoBehaviour {
    public GameObject Target;
    public Score score;
    public float genDistance;

    [Header("background")]
    public float bgOffset;
    public float bgFactor;
    public int layerPos;

    public int BiomsCount;
    public Biom[] bioms;

    [SerializeField] int currentBiom = -1;
    [SerializeField] float currentBiomSize;
    [SerializeField] float currentBiomFilling;
    [SerializeField] public GameObject lastTile;

    int target_distance;

    private void Awake() {
        foreach (var i in bioms) {
            foreach (var j in i.tiles) {
                j.GetComponent<TileInfo>().biome_name = i.name;
            }
        }
        if (PlayerPrefs.HasKey("DistanceToTargetCity") == false) { PlayerPrefs.SetInt("DistanceToTargetCity", 149); }
        target_distance = PlayerPrefs.GetInt("DistanceToTargetCity");
    }
    private int RandomInt(int minV, int maxV) {
        float i = Random.Range(minV, maxV);

        int j = Mathf.RoundToInt(i);
        j %= (maxV - minV);
        j += minV;

        return Random.Range(minV, maxV);
    }
    public void ChangeBiom(int i = -1) {
        if (currentBiom > 0) {
            if (bioms[currentBiom].end_tile != null) {
                GenTile(bioms[currentBiom].end_tile);
            }
        }
        if (i == -1) {
            int t = currentBiom;
            while (t == currentBiom || bioms[currentBiom].canGen == false) currentBiom = RandomInt(0, bioms.Length);
            if (PlayerPrefs.GetInt("QrertonDF") == 0) currentBiom = 0;
            //if (bioms[currentBiom].canGen == false) {
            //    ChangeBiom();
            //    return;
            //}
        } else {
            currentBiom = i;
        }
        currentBiomSize = RandomInt(bioms[currentBiom].minBiomSize, bioms[currentBiom].maxBiomSize);
        currentBiomFilling = 0;
        if (bioms[currentBiom].start_tile != null) {
            GenTile(bioms[currentBiom].start_tile);
        }
    }
    public void GenTile(GameObject example) {
        GameObject tmpTile = null;
        tmpTile = Instantiate(example);
        tmpTile.GetComponent<TileInfo>().generator = gameObject;
        tmpTile.GetComponent<TileInfo>().isExample = false;

        Vector3 newTilePos;
        if (lastTile == null) {
            newTilePos = transform.position;
            newTilePos += (tmpTile.GetComponent<TileInfo>().tileLength) / 2;
        } else {
            newTilePos = lastTile.transform.position;
            newTilePos += (lastTile.GetComponent<TileInfo>().tileLength) / 2;
            newTilePos += (tmpTile.GetComponent<TileInfo>().tileLength) / 2;
        }

        tmpTile.transform.position = newTilePos;
        lastTile = tmpTile;
    }
    public void GenNewTile() {
        if (currentBiom == -1) ChangeBiom();
        if (currentBiomSize == currentBiomFilling) ChangeBiom();
        score.GetDist();
        currentBiomFilling += 1;

        if (lastTile != null) {
            var t = lastTile.transform.position + lastTile.GetComponent<TileInfo>().tileLength / 2;
            if (score.GetDist(t) >= target_distance - 20) {
                ChangeBiom(5);
            }
        }
        GenTile(bioms[currentBiom].tiles[RandomInt(0, bioms[currentBiom].tiles.Count)]);
    }
    public void SrartGen() {
        for (int i = 0; i < 2; ++i) {
            GenNewTile();
        }
    }
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if ((lastTile.transform.position.x - Target.transform.position.x) < genDistance) {
            GenNewTile();
        }
    }
}

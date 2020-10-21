using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Biom
{
    public string name;
    public bool canGen;
    public int minBiomSize = 2;
    public int maxBiomSize = 5;
    public List<GameObject> tiles;
}

public class TileGenerator : MonoBehaviour
{
    public GameObject Target;
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

    private int RandomInt(int minV, int maxV)
    {
        float i = Random.Range(minV, maxV);

        int j = Mathf.RoundToInt(i);
        j %= (maxV - minV);
        j += minV;

        return Random.Range(minV, maxV);
    }
    public void ChangeBiom(int i = -1) {
        if(i == -1){
            currentBiom = RandomInt(0, bioms.Length);
            if (bioms[currentBiom].canGen == false) {
                ChangeBiom();
                return;
            }
        }
        else
        {
            currentBiom = i;
        }
        currentBiomSize = RandomInt(bioms[currentBiom].minBiomSize, bioms[currentBiom].maxBiomSize);
        currentBiomFilling = 0;
    }
    public void GenNewTile()
    {
        if (currentBiom == -1) ChangeBiom();
        if (currentBiomSize == currentBiomFilling) ChangeBiom();

        currentBiomFilling += 1;

        GameObject tmpTile = null;

        tmpTile = Instantiate(bioms[currentBiom].tiles[RandomInt(0, bioms[currentBiom].tiles.Count)]);
        tmpTile.GetComponent<TileInfo>().generator = gameObject;
        tmpTile.GetComponent<TileInfo>().isExample = false;

        Vector3 newTilePos;
        if (lastTile == null)
        {
            newTilePos = transform.position;
            newTilePos += (tmpTile.GetComponent<TileInfo>().tileLength) / 2;
        }
        else
        {
            newTilePos = lastTile.transform.position;
            newTilePos += (lastTile.GetComponent<TileInfo>().tileLength) / 2;
            newTilePos += (tmpTile.GetComponent<TileInfo>().tileLength) / 2;
        }

        tmpTile.transform.position = newTilePos;
        lastTile = tmpTile;
    }

    void Start()
    {
        for (int i = 0; i < 2; ++i)
        {
            GenNewTile();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((lastTile.transform.position.x - Target.transform.position.x) < genDistance)
        {
            GenNewTile();
        }
    }


}

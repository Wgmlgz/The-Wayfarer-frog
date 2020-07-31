using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Biom
{
    public string name;
    public List<GameObject> tiles;
}

public class TileGenerator : MonoBehaviour
{
    public GameObject Target;
    public float genDistance;

    [Header("background")]
    public bool isBackground;
    public float bgOffset;
    public float bgFactor;
    public int layerPos;

    public int BiomsCount;
    public Biom[] bioms;
    public int minBiomSize = 2;
    public int maxBiomSize = 5;

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
    public void ChangeBiom()
    {
        currentBiom = RandomInt(0, BiomsCount);
        currentBiomSize = RandomInt(minBiomSize, maxBiomSize);
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
        try
        {
            if (isBackground)
            {
                var tmp = tmpTile.GetComponent<ParalaxEffect>();
                tmp.ResetPos(newTilePos);
                tmp.factor = bgFactor;
                tmp.forceYOffset = bgOffset;

                tmpTile.GetComponentInChildren<SpriteRenderer>().sortingOrder = layerPos;
            }
        }
        finally
        {

        }
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

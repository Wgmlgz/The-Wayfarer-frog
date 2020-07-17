using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    public GameObject Target;
    public float genDistance;

    public int BiomsCount;
    public GameObject[] biomTest1;
    public GameObject[] biomTest2;
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
        if(currentBiom == -1) ChangeBiom();
        if(currentBiomSize == currentBiomFilling) ChangeBiom();

        currentBiomFilling += 1;

        GameObject tmpTile = null;
        if(currentBiom == 0)
        {
            tmpTile = Instantiate(biomTest1[RandomInt(0, biomTest1.Length)]);
        }
        else if (currentBiom == 1)
        {
            tmpTile = Instantiate(biomTest2[RandomInt(0, biomTest2.Length)]);
        }
        tmpTile.GetComponent<TileInfo>().generator = gameObject;
        tmpTile.GetComponent<TileInfo>().isExample = false;

        Vector3 newTilePos;
        if (lastTile == null) newTilePos = transform.position;
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
        for(int i = 0; i < 2; ++i)
        {
            GenNewTile();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if((lastTile.transform.position.x - Target.transform.position.x) < genDistance)
        {
            GenNewTile();
        }
    }


}

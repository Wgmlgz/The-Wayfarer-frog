using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo : MonoBehaviour
{
    public bool isExample = true;
    public GameObject generator;
    public Vector3 tileLength;

    private void Update()
    {
        if (isExample == false)
        {
            if (generator.GetComponent<TileGenerator>().Target.transform.position.x - transform.position.x
                > generator.GetComponent<TileGenerator>().genDistance
                && gameObject != generator.GetComponent<TileGenerator>().lastTile)
            {
                Destroy(gameObject);
            }
        }
    }
}

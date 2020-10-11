using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    [Range(0, 7)] public int phase = 1;
    public Sprite[] sequence;
    private void Start()
    {
        phase = Random.Range(0, 8);
    }
    private void Update()
    {
        GetComponent<SpriteRenderer>().sprite = sequence[phase];
    }
}

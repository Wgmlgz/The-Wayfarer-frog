using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomActivator : MonoBehaviour
{
    [Range(0, 1)] public float chance = .5f;
    
    public void Refresh()
    {
        gameObject.SetActive(Random.value < chance);
    }

    private void Awake()
    {
        Refresh();
    }
}

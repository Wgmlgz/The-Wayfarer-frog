using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomActivator : MonoBehaviour
{
    [Range(0, 1)] public float chance = .5f;
    public GameObject[] variations;

    public void Refresh()
    {
        if (variations.Length > 0)
        {
            gameObject.SetActive(Random.value < chance);
        }
        else
        {
            foreach (var i in variations)
            {
                i.SetActive(false);
            }
            variations[Random.Range(0, variations.Length)].SetActive(Random.value < chance);
        }
    }

    private void Awake()
    {
        Refresh();
    }
}

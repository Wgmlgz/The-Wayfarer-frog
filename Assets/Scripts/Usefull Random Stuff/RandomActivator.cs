using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomActivator : MonoBehaviour
{
    [Range(0, 1)] public float chance = .5f;
    public GameObject[] variations;

    public void Refresh()
    {
        if (Time.timeSinceLevelLoad < 1f) {
            return;
        }
        if (variations.Length <= 0)
        {
            gameObject.SetActive(Random.value < chance);
        }
        else
        {
            int tmp = Random.Range(0, variations.Length);
            for (int i = 0; i < variations.Length; i++)
            {
                if(i == tmp && Random.value < chance)
                {
                    variations[i].SetActive(true);
                }
                else
                {
                    variations[i].SetActive(false);
                }
            }
        }
    }

    private void Awake()
    {
        Refresh();
    }
}
